using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LiteNetLib;
using LiteNetLib.Utils;


namespace BloomShootServer;

public class Server : IDisposable
{
    private readonly EventBasedNetListener listener = new EventBasedNetListener();
    private readonly NetManager server;
    private bool isRunning = false;
    private bool isDisposed = false;
    private int port;
    
    private string[] _serverReceivedMessages;
    public string[] ServerReceivedMessages => _serverReceivedMessages;

    public Server(string password, int port)
    {
        _serverReceivedMessages = [];
        
        server = new NetManager(listener);
        if (!server.Start(port))
            throw new Exception("Failed to start server");
            
        listener.ConnectionRequestEvent += request =>
        {
            if (server.ConnectedPeersCount < 2)
                request.AcceptIfKey(password);
            else
                request.Reject();
        };

        listener.PeerConnectedEvent += peer =>
        {
            Console.WriteLine($"Client connected: {peer.Address}");
            List<string> serverReceivedMessagesList = _serverReceivedMessages.ToList();
            serverReceivedMessagesList.Add($"Client connected: {peer.Address}");
            _serverReceivedMessages = serverReceivedMessagesList.ToArray();
            SendMessageToClient(peer, $"{server.ConnectedPeersCount}");
        };

        listener.NetworkReceiveEvent += OnNetworkReceive;

        isRunning = true;
    }

    public void SendMessageToClient(NetPeer peer, string message)
    {
        if (peer.ConnectionState == ConnectionState.Connected)
        {
            NetDataWriter writer = new NetDataWriter();
            writer.Put(message);
            peer.Send(writer, DeliveryMethod.ReliableOrdered);
        }
    }

    public void BroadcastMessage(string message)
    {
        NetDataWriter writer = new NetDataWriter();
        writer.Put(message);
        server.SendToAll(writer, DeliveryMethod.ReliableOrdered);
    }

    private void OnNetworkReceive(NetPeer peer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
    {
        try
        {
            string message = reader.GetString();
            if (message != "")
            {
                Console.WriteLine($"Server received from {peer.Address}: {message}");

                List<string> serverReceivedMessagesList = _serverReceivedMessages.ToList();
                serverReceivedMessagesList.Add(message);
                _serverReceivedMessages = serverReceivedMessagesList.ToArray();

                // Echo the message back to all clients
                //BroadcastMessage($"Client {peer.Address} says: {message}");
                BroadcastMessage(message);
            }
        }
        finally
        {
            reader.Recycle();
        }
    }

    public void Update()
    {
        if (isRunning && !isDisposed)
        {
            server.PollEvents();
        }
    }

    public void StopServer()
    {
        if (!isDisposed)
        {
            isRunning = false;
            server.Stop();
        }
    }

    public void Dispose()
    {
        if (!isDisposed)
        {
            StopServer();
            isDisposed = true;
        }
        GC.SuppressFinalize(this);
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