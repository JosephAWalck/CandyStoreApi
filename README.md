# Candy Store REST API 

The Candy Store API is a RESTful API that offers the functionality of an online Candy Store back-end and database. Available Routes include:

- Login and Registration for Authentication and Authorization of users. Auth service is JWT-based and performed local to the application.
    - Roles: 0 == "Admin", 1 == "User"
- CRUD operations on Candy objects. POST, PUT, and DELETE require the "Admin" role. GET and GET by ID are unprotected routes accessible to anyone.
- Get, Add to, and Remove from a user's Shopping Cart.
- Submit an Order.

# Authentication

## Register User

### Request

`POST api/Accounts/register`

    {
      "email": "string",
      "password": "string",
      "firstName": "string",
      "lastName": "string",
      "role": 0
    }
    
### Response

    Status Code: 200 OK
    Content-Type: application/json; charset=utf-8

    {
      "token": "string"
    }

## Login

### Request

`POST /api/Accounts/login`
    
    {
      "email": "string",
      "password": "string"
    }

### Response

    Status Code: 200 OK
    Content-Type: application/json; charset=utf-8

    {
      "token": "string"
    }

### Error

    Status Code: 400 Bad Request
    Content-Type: text/plain; charset=utf-8

    "Couldn't sign in"

# Candies

## Get Candies

### Request

`GET /api/Candies?category=categoryName`

### Response

    Status Code: 200 OK
    Content-Type: application/json; charset=utf-8
    
    [
      {
        "candyId": int,
        "name": "string",
        "description": "string",
        "price": int,
        "imageURL": "string",
        "inventory": int,
        "categoryID": 0
      }
    ]

## Get Candy By Id

### Request

`GET /api/Candies/{id:int}`

### Response

    Status Code: 200 OK
    Content-Type: application/json; charset=utf-8

    {
      "candyId": int,
      "name": "string",
      "description": "string",
      "price": decimal,
      "imageURL": "string",
      "inventory": int,
      "categoryID": int
    }

### Error

    Status Code: 404 Not Found
    Content-Type: text/plain; charset=utf-8

    {
      "title": "Not Found"
      "status": 404,
    }

## Create Candy 

### Request

`POST /api/Candies`
`Protected - Admin Access Only`

    {
      "name": "string",
      "description": "string",
      "price": 0,
      "imageURL": "string",
      "inventory": 0,
      "categoryID": 0
    }

### Response

    Status Code: 200 OK
    Content-Type: application/json; charset=utf-8

    {
      "name": "string",
      "description": "string",
      "price": decimal,
      "imageURL": "string",
      "inventory": int,
      "categoryID": int
    }

### Error

    Status Code: 401 Unauthorized

## Update Candy

### Request

`PUT /api/Candies/{id:int}`
`Protected - Admin Access Only`

### Response

    Status Code: 200 OK
    Content-Type: application/json; charset=utf-8

    {
      "name": "string",
      "description": "string",
      "price": 0,
      "imageURL": "string",
      "inventory": 0,
      "categoryID": 0
    }

### Error

    Status Code: 401 Unauthorized

## Delete Candy

### Request

`DELETE /api/Candies/{id:int}`
`Protected - Admin Access Only`

### Response

    Status Code: 200 OK
    Content-Type: application/json; charset=utf-8

### Error

    Status Code: 401 Unauthorized

    ------------------------------

    Status Code: 404 Not Found
    Content-Type: text/plain; charset=utf-8

    {
      "title": "Not Found"
      "status": 404,
    }

# Shopping Cart

## Get Shopping Cart

### Request

`GET /api/ShoppingCart`

### Response

    Status Code: 200 OK
    Content-Type: application/json; charset=utf-8

    [
      {
        "shoppingCartItemId": int,
        "candy": {
          "candyId": int,
          "name": "string",
          "description": "string",
          "price": decimal,
          "imageURL": "string",
          "inventory": int,
          "categoryID": int
        },
        "quantity": int,
        "shoppingCartId": "string"
      }
    ]

## Add to Shopping Cart

### Request

`PUT api/ShoppingCart/add?CandyID=int`

### Response

    Status Code: 200 OK
    Content-Type: application/json; charset=utf-8

    [
      {
        "shoppingCartItemId": int,
        "candy": {
          "candyId": int,
          "name": "string",
          "description": "string",
          "price": decimal,
          "imageURL": "string",
          "inventory": int,
          "categoryID": int
        },
        "quantity": int,
        "shoppingCartId": "string"
      }
    ]
    
## Remove From Shopping Cart

### Request

`PUT api/ShoppingCart/remove?CandyID=int`

### Response

    Status Code: 200 OK
    Content-Type: application/json; charset=utf-8

    [
      {
        "shoppingCartItemId": int,
        "candy": {
          "candyId": int,
          "name": "string",
          "description": "string",
          "price": decimal,
          "imageURL": "string",
          "inventory": int,
          "categoryID": int
        },
        "quantity": int,
        "shoppingCartId": "string"
      }
    ]

# Order

## Place Order

### Request

`POST /api/Order/Checkout`

    {
      "firstName": "string",
      "lastName": "string",
      "addressLine1": "string",
      "addressLine2": "string",
      "zipCode": "string",
      "city": "string",
      "state": "string",
      "country": "string",
      "phoneNumber": "string",
      "email": "string"
    }

### Response

    Status Code: 200 OK
    Content-Type: application/json; charset=utf-8

    {
      "orderId": int,
      "orderDetails": [
        {
          "orderDetailId": int,
          "orderId": int,
          "candyId": int,
          "quantity": int,
          "price": int,
          "candy": {
            "candyId": int,
            "name": "string",
            "description": "string",
            "price": decimal,
            "imageURL": "string",
            "inventory": int,
            "categoryID": int
          }
        }
      ],
      "firstName": "string",
      "lastName": "string",
      "addressLine1": "string",
      "addressLine2": "string",
      "zipCode": "string",
      "city": "string",
      "state": "string",
      "country": "string",
      "phoneNumber": "string",
      "email": "string",
      "orderTotal": decimal,
      "orderPlaced": DateTime (ISO 8601)
    }
    
