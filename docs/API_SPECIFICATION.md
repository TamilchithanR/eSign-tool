# eSignature Platform - API Specification

## API Overview

Base URL: `https://api.esignature.com/v1`  
Authentication: Bearer JWT Token  
Content-Type: `application/json`

---

## 1. Authentication Endpoints

### 1.1 Login
```
POST /auth/login
```

**Request Body:**
```json
{
  "email": "user@example.com",
  "password": "securePassword123"
}
```

**Response (200):**
```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIs...",
  "refreshToken": "eyJhbGciOiJIUzI1NiIs...",
  "expiresIn": 900,
  "user": {
    "userId": "550e8400-e29b-41d4-a716-446655440000",
    "email": "user@example.com",
    "firstName": "John",
    "lastName": "Doe"
  }
}
```

---

## 2. Document Management Endpoints

### 2.1 Upload Document
```
POST /documents/upload
```

**Headers:**
- Authorization: Bearer {token}
- Content-Type: multipart/form-data

**Request Parameters:**
- file: PDF file (max 50MB)
- title: Document title
- description: (optional) Document description

**Response (201):**
```json
{
  "documentId": "550e8400-e29b-41d4-a716-446655440001",
  "title": "Contract Agreement",
  "status": "Draft",
  "createdAt": "2025-06-05T10:30:00Z",
  "blobStoragePath": "documents/org-123/doc-123.pdf"
}
```

### 2.2 Get Documents
```
GET /documents?status=Draft&skip=0&take=20
```

**Query Parameters:**
- status: (optional) Document status filter
- skip: Number of documents to skip (default: 0)
- take: Number of documents to return (default: 20)

**Response (200):**
```json
{
  "data": [
    {
      "documentId": "550e8400-e29b-41d4-a716-446655440001",
      "title": "Contract Agreement",
      "status": "Draft",
      "createdAt": "2025-06-05T10:30:00Z"
    }
  ],
  "totalCount": 150,
  "skip": 0,
  "take": 20
}
```

### 2.3 Get Document Details
```
GET /documents/{documentId}
```

**Response (200):**
```json
{
  "documentId": "550e8400-e29b-41d4-a716-446655440001",
  "title": "Contract Agreement",
  "status": "Sent",
  "createdAt": "2025-06-05T10:30:00Z",
  "recipients": [
    {
      "recipientId": "550e8400-e29b-41d4-a716-446655440010",
      "email": "signer@example.com",
      "role": "Signer",
      "status": "Pending",
      "signingOrder": 1
    }
  ],
  "fields": [
    {
      "fieldId": "550e8400-e29b-41d4-a716-446655440020",
      "fieldType": "Signature",
      "pageNumber": 1,
      "positionX": 100,
      "positionY": 500
    }
  ]
}
```

---

## 3. Recipient Management Endpoints

### 3.1 Add Recipients
```
POST /documents/{documentId}/recipients
```

**Request Body:**
```json
{
  "recipients": [
    {
      "email": "signer1@example.com",
      "role": "Signer",
      "signingOrder": 1,
      "firstName": "John",
      "lastName": "Smith"
    },
    {
      "email": "signer2@example.com",
      "role": "Signer",
      "signingOrder": 2
    }
  ]
}
```

**Response (201):**
```json
{
  "recipients": [
    {
      "recipientId": "550e8400-e29b-41d4-a716-446655440010",
      "email": "signer1@example.com",
      "status": "Pending"
    },
    {
      "recipientId": "550e8400-e29b-41d4-a716-446655440011",
      "email": "signer2@example.com",
      "status": "Pending"
    }
  ]
}
```

### 3.2 Update Recipient
```
PUT /documents/{documentId}/recipients/{recipientId}
```

**Request Body:**
```json
{
  "role": "Approver",
  "signingOrder": 2
}
```

**Response (200):**
```json
{
  "recipientId": "550e8400-e29b-41d4-a716-446655440010",
  "email": "signer1@example.com",
  "role": "Approver",
  "status": "Pending"
}
```

---

## 4. Signature Fields Endpoints

### 4.1 Add Signature Fields
```
POST /documents/{documentId}/fields
```

**Request Body:**
```json
{
  "fields": [
    {
      "recipientId": "550e8400-e29b-41d4-a716-446655440010",
      "fieldType": "Signature",
      "pageNumber": 1,
      "positionX": 100,
      "positionY": 500,
      "width": 200,
      "height": 50,
      "isRequired": true
    },
    {
      "recipientId": "550e8400-e29b-41d4-a716-446655440010",
      "fieldType": "Date",
      "pageNumber": 1,
      "positionX": 100,
      "positionY": 600,
      "isRequired": true
    }
  ]
}
```

**Response (201):**
```json
{
  "fields": [
    {
      "fieldId": "550e8400-e29b-41d4-a716-446655440020",
      "fieldType": "Signature",
      "pageNumber": 1,
      "positionX": 100,
      "positionY": 500,
      "isSigned": false
    }
  ]
}
```

---

## 5. Signing Endpoints

### 5.1 Get Signing Document
```
GET /signing/{accessToken}
```

**Response (200):**
```json
{
  "documentId": "550e8400-e29b-41d4-a716-446655440001",
  "title": "Contract Agreement",
  "recipientId": "550e8400-e29b-41d4-a716-446655440010",
  "recipientEmail": "signer@example.com",
  "fields": [
    {
      "fieldId": "550e8400-e29b-41d4-a716-446655440020",
      "fieldType": "Signature",
      "pageNumber": 1,
      "positionX": 100,
      "positionY": 500,
      "isSigned": false
    }
  ],
  "pdfPreviewUrl": "https://api.esignature.com/documents/123/preview"
}
```

### 5.2 Submit Signature
```
POST /signing/{accessToken}/sign
```

**Request Body:**
```json
{
  "signatures": [
    {
      "fieldId": "550e8400-e29b-41d4-a716-446655440020",
      "signatureType": "Draw",
      "signatureData": "data:image/png;base64,iVBORw0KGgoAAAANSUh...",
      "ipAddress": "192.168.1.1",
      "userAgent": "Mozilla/5.0..."
    }
  ]
}
```

**Response (200):**
```json
{
  "success": true,
  "message": "Document signed successfully",
  "documentStatus": "Completed",
  "completionCertificateUrl": "https://api.esignature.com/certificates/123"
}
```

---

## 6. Workflow Endpoints

### 6.1 Send for Signature
```
POST /documents/{documentId}/send
```

**Request Body:**
```json
{
  "workflowType": "Sequential",
  "expirationDays": 30,
  "reminderDays": [3, 1],
  "message": "Please review and sign this agreement",
  "reminderFrequency": "Daily"
}
```

**Response (200):**
```json
{
  "workflowId": "550e8400-e29b-41d4-a716-446655440050",
  "documentStatus": "Sent",
  "sentAt": "2025-06-05T11:00:00Z",
  "recipientsNotified": 2
}
```

### 6.2 Resend to Recipient
```
POST /documents/{documentId}/recipients/{recipientId}/resend
```

**Response (200):**
```json
{
  "success": true,
  "message": "Invitation resent to signer@example.com",
  "sentAt": "2025-06-05T11:05:00Z"
}
```

---

## 7. Audit & Compliance Endpoints

### 7.1 Get Audit Log
```
GET /documents/{documentId}/audit-log?skip=0&take=50
```

**Response (200):**
```json
{
  "auditLogs": [
    {
      "auditId": "123456",
      "action": "Signed",
      "userId": "550e8400-e29b-41d4-a716-446655440010",
      "timestamp": "2025-06-05T11:30:00Z",
      "ipAddress": "192.168.1.1",
      "userAgent": "Mozilla/5.0...",
      "deviceInfo": {
        "browser": "Chrome",
        "os": "Windows 10",
        "deviceType": "Desktop"
      }
    }
  ],
  "totalCount": 45
}
```

### 7.2 Generate Audit Certificate
```
POST /documents/{documentId}/audit-certificate
```

**Response (200):**
```json
{
  "certificateId": "550e8400-e29b-41d4-a716-446655440060",
  "certificateUrl": "https://api.esignature.com/certificates/123/download",
  "generatedAt": "2025-06-05T11:35:00Z",
  "expiresAt": "2026-06-05T11:35:00Z"
}
```

---

## 8. Error Responses

### 8.1 Error Format
```json
{
  "error": {
    "code": "INVALID_REQUEST",
    "message": "Invalid document status",
    "details": "Cannot send document with status 'Completed'"
  }
}
```

### 8.2 Common Error Codes

| Code | Status | Description |
|------|--------|-------------|
| INVALID_REQUEST | 400 | Invalid request parameters |
| UNAUTHORIZED | 401 | Missing or invalid authentication |
| FORBIDDEN | 403 | Insufficient permissions |
| NOT_FOUND | 404 | Resource not found |
| CONFLICT | 409 | Resource conflict (e.g., duplicate email) |
| RATE_LIMIT_EXCEEDED | 429 | Rate limit exceeded |
| INTERNAL_ERROR | 500 | Server error |

---

## 9. Rate Limiting

- Per-user: 1,000 requests/hour
- Per-IP: 5,000 requests/hour
- Per-endpoint: Dynamic (100-1,000 requests/minute)

Headers in response:
```
X-RateLimit-Limit: 1000
X-RateLimit-Remaining: 999
X-RateLimit-Reset: 1654419600
```

---

**API Version**: 1.0  
**Last Updated**: 2025-06-05  
**Status**: Ready for Development
