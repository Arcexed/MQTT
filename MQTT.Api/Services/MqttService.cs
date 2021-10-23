using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MQTT.Api.Models;
using MQTT.Data;
using MQTTnet;
using MQTTnet.AspNetCore;
using MQTTnet.AspNetCore.AttributeRouting;
using MQTTnet.Client.Receiving;
using MQTTnet.Protocol;
using MQTTnet.Server;

namespace MQTT.Api.Services
{
    public class MqttService :
        IMqttServerConnectionValidator,
        IMqttApplicationMessageReceivedHandler,
        IMqttServerApplicationMessageInterceptor,
        IMqttServerStartedHandler,
        IMqttServerStoppedHandler,
        IMqttServerClientConnectedHandler,
        IMqttServerClientDisconnectedHandler,
        IMqttServerClientSubscribedTopicHandler,
        IMqttServerClientUnsubscribedTopicHandler
    {
        private readonly AppSettings _appSettings;
        private readonly LoggerService _loggerService;
        public IMqttServer Server;
        public List<Client> PreConnectedClients = new();
        public List<Client> ConnectedClients = new();
        
        public MqttService(AppSettings appSettings, LoggerService loggerService)
        {
            _appSettings = appSettings;
            _loggerService = loggerService;
        }
        
        public void ConfigureMqttServerOptions(AspNetMqttServerOptionsBuilder options)
        {
            // Configure the MQTT Server options here
            options.WithoutDefaultEndpoint();
            options.WithConnectionValidator(this);
            options.WithApplicationMessageInterceptor(this);
            // Enable Attribute Routing
            // By default, messages published to topics that don't match any routes are rejected. 
            // Change this to true to allow those messages to be routed without hitting any controller actions.
            options.WithAttributeRouting(true);
        }
        public void ConfigureMqttServer(IMqttServer mqtt)
        {
            Server = mqtt;
            mqtt.ApplicationMessageReceivedHandler = this;
            mqtt.StartedHandler = this;
            mqtt.StoppedHandler = this;
            mqtt.ClientConnectedHandler = this;
            mqtt.ClientDisconnectedHandler = this;
            mqtt.ClientSubscribedTopicHandler = this;
            mqtt.ClientUnsubscribedTopicHandler = this;
        }

        public Task ValidateConnectionAsync(MqttConnectionValidatorContext context)
        {
            return Task.Run(() =>
            {
                // простая конструкция using как делали в wpf 

                using (var _db = new MQTTDbContext(Test.Options))
                {
                    var deviceName = context.Username;
                    var mqttToken = context.Password;
                
                    var device = _db.Devices.FirstOrDefault(d => d.Name == deviceName && d.MqttToken == mqttToken);
                    if (device != null)
                    {
                        _loggerService.Log($"Validate success user id: {context.ClientId} username: {context.Username} password: {context.Password}");
                    
                        _loggerService.LogEventDevice(device,$"Validate success user id: {context.ClientId} username: {context.Username} password: {context.Password}");
                    

                        if (ConnectedClients.Any(d => d.Username == deviceName))
                        {
                            _loggerService.Log($"User id is contains in ConnectedClients: {context.ClientId} username: {context.Username} password: {context.Password}");
                            _loggerService.LogEventDevice(device,$"User id is contains in ConnectedClients: {context.ClientId} username: {context.Username} password: {context.Password}");
                        }
                        var client = new Client()
                        {
                            Id = context.ClientId,
                            Ip = context.Endpoint,
                            Username = context.Username,
                            MqttToken = context.Password
                        };
                        PreConnectedClients.Add(client);
                        context.ReasonCode = MqttConnectReasonCode.Success;
                        return;
                    }
                    else
                    {
                        _loggerService.Log($"Error validate (not found device in db) user id: {context.ClientId} username: {context.Username} password: {context.Password}");
                        context.ReasonCode = MqttConnectReasonCode.NotAuthorized;
                    }
                }
                
               
            });
        }

        public Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            return Task.Run(() =>
            {
                _loggerService.Log($"Received MQTT Message Logged " +
                                   $"- Topic = {eventArgs.ApplicationMessage.Topic}" +
                                   $"- Payload = {Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload)}" +
                                   $"- QoS = {eventArgs.ApplicationMessage.QualityOfServiceLevel}" +
                                   $"- Retain = {eventArgs.ApplicationMessage.Retain}");
            });
        }

        public Task InterceptApplicationMessagePublishAsync(MqttApplicationMessageInterceptorContext context)
        {
            return Task.Run(() => { _loggerService.Log($"InterceptApplicationMessagePublishAsync Handler Triggered"); });
        }

        public Task HandleServerStartedAsync(EventArgs eventArgs)
        {
            return Task.Run(() => { _loggerService.Log($"HandleServerStartedAsync Handler Triggered"); });
        }

        public Task HandleServerStoppedAsync(EventArgs eventArgs)
        {
            
            return Task.Run(() => { _loggerService.Log($"HandleServerStoppedAsync Handler Triggered"); });
        }

        public Task HandleClientConnectedAsync(MqttServerClientConnectedEventArgs eventArgs)
        {
            return Task.Run(() =>
            {
                _loggerService.Log($"HandleClientConnectedAsync Handler Triggered");
                
                
                var clientId = eventArgs.ClientId;
                var client = PreConnectedClients.FirstOrDefault(d => d.Id == clientId);
                if (client != null)
                {
                    ConnectedClients.Add(client);
                }
                else
                {
                    ConnectedClients.Add(new Client()
                    {
                     Id=clientId   
                    });
                }
                PreConnectedClients.Remove(client);
                _loggerService.Log($"MQTT Client Connected - ClientID = {clientId}");
            });
        }

        public Task HandleClientDisconnectedAsync(MqttServerClientDisconnectedEventArgs eventArgs)
        {
            return Task.Run(() => { 
                _loggerService.Log($"HandleClientDisconnectedAsync Handler Triggered");
                
                var clientId = eventArgs.ClientId;
                var client = ConnectedClients.First(d => d.Id == clientId);
                ConnectedClients.Remove(client);
                
                _loggerService.Log($"MQTT Client Disconnected - ClientID = {clientId} Username = {client.Username} MqttToken = {client.MqttToken}");
            });        }

        public Task HandleClientSubscribedTopicAsync(MqttServerClientSubscribedTopicEventArgs eventArgs)
        {
            return Task.Run(() => { _loggerService.Log("ClientSubscribedTopicHandler Handler Triggered"); });
        }

        public Task HandleClientUnsubscribedTopicAsync(MqttServerClientUnsubscribedTopicEventArgs eventArgs)
        {
            return Task.Run(() => { _loggerService.Log("ClientSubscribedTopicHandler Handler Triggered"); });
        }
    }
}