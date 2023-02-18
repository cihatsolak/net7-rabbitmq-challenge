# RabbitMQ Tutorials

## Introduction

This repository contains code examples for RabbitMQ using the .NET client. The purpose is to refresh my knowledge on RabbitMQ and test its features by trying out various scenarios.

## Prerequisites

- RabbitMQ installed and running locally
- Visual Studio (or any .NET development environment) installed

## Getting Started

1. Clone this repository
2. Open the solution file in Visual Studio
3. Build the solution
4. Run the `Publisher` project to send messages to RabbitMQ
5. Run the `Consumer` project to receive messages from RabbitMQ

## Example Scenarios

### Direct Exchange

- Send a message to a queue
- Receive a message from a queue

### Fanout Exchange

- Send a message to a fanout exchange
- Receive a message from a queue bound to the fanout exchange

### Topic Exchange

- Send a message to a topic exchange with a routing key
- Receive a message from a queue bound to the topic exchange with a binding key that matches the routing key of the sent message

## Conclusion

These code examples demonstrate the basic functionalities of RabbitMQ. The possibilities are endless, and these examples can be expanded and modified to suit specific use cases.
