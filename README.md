# Microservices-With-Kubernetes
**Build microservices with Docker and orchestrates using Kubernetes(Rabbit MQ)**

# Project architechture diagram with kubernetes
![Architechture Image](Kubernets-Architecture-Diagram.jpeg)

# Kubernetes
* Built by Google now maintained bt the Cloud Native Foundation
* Often referred to as "K8S"
* Container Orchestrator
* 2 broad user profiles
  1. Developer
  2. Administrator

# Docker

#### _Docker is a containerization platform, meaning that it enables you to package your applications into Images and run them as "Containers" on any platform that can run docker._

![Referance Image](Docker-Defination.png)

# Platform service architechture diagram
![Platform service](PlatformService.jpeg)

# Messaging 
### Synchronous & Asynchronous Messaging
 **Synchronous Messaging**
* Request/ Response cycle
* Requester will "wait" for response
* Externally facing services usually synchronous (e.g http requests)
* Services usually need to "know" about each other
* We are using 2 forms:
  * Http
  * Grpc
