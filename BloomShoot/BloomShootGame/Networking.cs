using System;
using System.IO;
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
    
    public void Update()
    {
        client?.PollEvents();
    }
    
    public void SendMessage(string message)
    {
        if (serverPeer != null && serverPeer.ConnectionState == LiteNetLib.ConnectionState.Connected)
        {
            NetDataWriter writer = new NetDataWriter();
            writer.Put(message);
            serverPeer.Send(writer, DeliveryMethod.ReliableOrdered);
        }
        else
        {
            Console.WriteLine("Cannot send message - not connected to server");
        }
    }
    
    private void OnNetworkReceive(NetPeer peer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
    {
        Console.WriteLine("We got: {0}", reader.GetString(100));
        reader.Recycle();
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

struct NetworkSettings
{
    public string HostIP;
    public int Port;
    public bool IsServer;
    public string Password;
    
    public NetworkSettings(string IP, int port, bool isServer, string password)
    {
        HostIP = IP;
        Port = port;
        IsServer = isServer;
        Password = password;

        if (!File.Exists("network_settings.txt"))
        {
            File.WriteAllText("network_settings.txt", $"{HostIP}\n{Port}\n{Password}\n{IsServer}");
        }
        else
        {
            string[] lines = File.ReadAllLines("network_settings.txt");
            HostIP = lines[0];
            Port = int.Parse(lines[1]);
            Password = lines[2];
            Console.Write(lines[3]);
            if (lines[3] == "true" || lines[3] == "True") isServer = true;
            else isServer = false;
        }
        
        Console.WriteLine($"Using the following network settings");
        Console.WriteLine($"Adress: {HostIP}:{Port}"); Console.WriteLine($"Password: {Password}"); Console.WriteLine($"Runs as server: {isServer}");
    }
}