# DH Vehicle Inventory API - Contract Documentation

## API Versioning Strategy

- Current Version: v1
- Versioning Approach: URL path prefix (/api/DH_Vehicles)

---

## Endpoint Contracts

### 1. GET /api/DH_Vehicles

- Method: GET
- Request Body: None
- Success: 200 OK
- Response: Array of vehicle objects

---

### 2. GET /api/DH_Vehicles/{id}

- Method: GET
- Path Param: id (int, required)
- Success: 200 OK
- Error: 404 Not Found

---

### 3. POST /api/DH_Vehicles

- Method: POST
- Content-Type: application/json
- Success: 201 Created
- Error: 400 Bad Request

Request Body:

| Field | Type | Rules |
|---|---|---|
| vehicleCode | string | Required, non-empty, max 50 characters |
| locationId | int | Required, must be greater than 0 |
| vehicleType | int | Required, 1=Sedan, 2=SUV, 10=Truck, 4=Van |

---

### 4. PUT /api/DH_Vehicles/{id}/status

- Method: PUT
- Path Param: id (int, required)
- Content-Type: application/json
- Success: 200 OK
- Errors: 400 Bad Request, 404 Not Found

Request Body:

| Field | Type | Rules |
|---|---|---|
| status | int | Required, 1=Available, 2=Reserved, 3=Rented, 4=Maintenance |

Domain Rules Enforced:

| From | To | Allowed |
|---|---|---|
| Available | Reserved | Yes |
| Available | Rented | Yes |
| Available | Maintenance | Yes |
| Reserved | Rented | No |
| Reserved | Maintenance | No |
| Rented | Reserved | No |
| Rented | Maintenance | No |
| Maintenance | Reserved | No |
| Maintenance | Rented | No |

---

### 5. DELETE /api/DH_Vehicles/{id}

- Method: DELETE
- Path Param: id (int, required)
- Success: 204 No Content
- Error: 404 Not Found

---

## Response Formats

### Success (Single Vehicle)
```json
{
  "id": 1,
  "vehicleCode": "TOY-CAM-001",
  "locationId": 1,
  "vehicleType": "Sedan",
  "status": "Available"
}
```

### Error
```json
{
  "error": "Vehicle not found."
}
```

### Validation Error
```json
{
  "errors": ["Vehicle code is required.", "Location ID must be a positive number."]
}
```