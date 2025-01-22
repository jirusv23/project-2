using System;
using System.Text.Json;
using LiteNetLib;
using LiteNetLib.Utils;

namespace BloomShootGame;

public class Client
{
    private EventBasedNetListener listener = new EventBasedNetListener();
    private NetManager client;
    private bool isRunning = false;
    public bool IsRunning => isRunning;
    private NetPeer serverPeer;

    private string ownID; public string OwnID => ownID;
    
    // Event handler for receiving player state updates
    public event Action<PlayerStateMessage> OnPlayerStateReceived;

    // Add connection state tracking
    public enum ConnectionState
    {
        Disconnected,
        Connecting,
        Connected,
        Failed
    }
    
    private ConnectionState _connectionState = ConnectionState.Disconnected;
    public ConnectionState CurrentState => _connectionState;
    public string LastError { get; private set; } = "";
    
    public Client(string password, string hostIP)
    {
        try
        {
            client = new NetManager(listener);
            if (!client.Start())
            {
                _connectionState = ConnectionState.Failed;
                LastError = "Failed to start client";
                throw new Exception(LastError);
            }

            _connectionState = ConnectionState.Connecting;
            Console.WriteLine($"Attempting connection to IP: {hostIP} with password: {password}");
            
            client.Connect(hostIP, 9050, password);
            
            // Setup event handlers
            listener.NetworkReceiveEvent += OnNetworkReceive;
            
            listener.PeerConnectedEvent += peer =>
            {
                Console.WriteLine("Connected to server!");
                serverPeer = peer;
                isRunning = true;
                _connectionState = ConnectionState.Connected;
            };
            
            listener.PeerDisconnectedEvent += (peer, disconnectInfo) =>
            {
                Console.WriteLine($"Disconnected from server: {disconnectInfo.Reason}");
                isRunning = false;
                _connectionState = ConnectionState.Disconnected;
                LastError = disconnectInfo.Reason.ToString();
            };
            
            listener.NetworkErrorEvent += (endPoint, error) =>
            {
                Console.WriteLine($"Network error: {error}");
                _connectionState = ConnectionState.Failed;
                LastError = error.ToString();
            };
        }
        catch (Exception ex)
        {
            _connectionState = ConnectionState.Failed;
            LastError = ex.Message;
            throw;
        }
    }
    
    // Generic method to send any JSON-serializable object
    public void SendJson<T>(T data)
    {
        if (serverPeer == null || serverPeer.ConnectionState != LiteNetLib.ConnectionState.Connected) 
        {
            Console.WriteLine("Cannot send message - not connected to server");
            return;
        }

        try
        {
            string jsonString = JsonSerializer.Serialize(data);
            NetDataWriter writer = new NetDataWriter();
            writer.Put(jsonString);
            serverPeer.Send(writer, DeliveryMethod.ReliableOrdered);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending JSON: {ex.Message}");
        }
    }
    
    // Example method specifically for sending player state
    public void SendPlayerState(float x, float y, float rotation, string playerId)
    {
        var playerState = new PlayerStateMessage
        {
            X = x,
            Y = y,
            Rotation = rotation,
            PlayerID = playerId
        };
        
        SendJson(playerState);
    }
    
    private void OnNetworkReceive(NetPeer peer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
    {
        try
        {
            string jsonString = reader.GetString();

            if (jsonString == "1" || jsonString == "2")
            {
                ownID = $"player{jsonString}";
            }
            else
            {
                var playerState = JsonSerializer.Deserialize<PlayerStateMessage>(jsonString);
                if (playerState != null)
                {
                    OnPlayerStateReceived?.Invoke(playerState);
                }
            }
            
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing received JSON: {ex.Message}");
        }
        finally
        {
            reader.Recycle();
        }
    }
    
    public void Update()
    {
        if (client != null)
        {
            client.PollEvents();
        }
    }
    
    public void StopClient()
    {
        isRunning = false;
        if (client != null)
        {
            client.Stop();
            _connectionState = ConnectionState.Disconnected;
        }
    }
}

public class PlayerStateMessage
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Rotation { get; set; }
    public string PlayerID { get; set; }
}