<div align="center">
  <a href="https://github.com/vanthang24803/microservice-store">
    <img src="https://theme.hstatic.net/200000294254/1001077164/14/favicon.png?v=325" alt="logo" width="100" height="100">
  </a>
</div>

<h2 align="center">AMAK Store API</h2>

<p align="center">An e-commerce  API built from Microservice architecture and ASP .NET Core Version 8</p>


## Table of Contents

  <ol>
      <br />
    <li>
    <a href="#about-the-project">About Project</a></li>
    <li><a href="#installation">Installation</a></li>
    <li><a href="#in-development">In Development</a></li>
  </ol>

<!-- ABOUT THE PROJECT -->

### ABOUT THE PROJECT:
<img src="https://middleware.io/wp-content/uploads/2021/09/How-Microservices-architecture-works-1024x786.jpg" >

<p>This is an eCommerce API built using a microservices architecture, ASP.NET Core, and Docker. It consists of three main services: Order, Product, and Auth. The Order service handles all order-related operations, the Product service manages product information, and the Auth service is responsible for authentication and authorization. The API Gateway, implemented using Ocelot, routes requests to the appropriate microservices. The system uses PostgreSQL as its database, providing robust data management and storage capabilities. All these components are containerized using Docker, ensuring easy deployment and scalability. This setup provides a highly modular, scalable, and efficient eCommerce solution.</p>

## Installation

The local configuration for API was setup to be as simple as possible for the end-user. <br />
Follow the steps below to get started with API.

#### Prerequisites:
You must have API installed and running!

#### Documentaion:
  <ul>
    <li>
    <a href="../APIGateWay/README.md">API Gateway</a></li>
    <li><a href="../Auth/README.md">Auth Service</a></li>
    <li><a href="../Product/README.md">Product Service</a></li>
    <li><a href="../Order/README.md">Order Service</a></li>
  </ul>

#### STEP 1 — Clone the repository

```sh
git clone[ https://github.com/open-source-labs/Docketeer.git](https://github.com/vanthang24803/microservice-store.git)
```

#### STEP 2 — Docker compose up

Making sure you're in your API directory, run:
```sh
docker compose up
```

#### STEP 3 — Navigate to localhost:8080 ( API Gateway)

```sh
http://localhost:8080
```




