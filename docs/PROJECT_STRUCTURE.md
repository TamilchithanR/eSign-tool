# eSignature Platform - Project Structure

## Directory Layout

```
eSign-tool/
├── backend/
│   ├── src/
│   │   ├── eSignature.API/                          # Main API entry point
│   │   │   ├── Controllers/
│   │   │   │   ├── AuthenticationController.cs
│   │   │   │   ├── DocumentsController.cs
│   │   │   │   ├── RecipientsController.cs
│   │   │   │   ├── SignatureFieldsController.cs
│   │   │   │   ├── WorkflowsController.cs
│   │   │   │   ├── AuditController.cs
│   │   │   │   └── TemplatesController.cs
│   │   │   ├── Middleware/
│   │   │   │   ├── ErrorHandlingMiddleware.cs
│   │   │   │   ├── LoggingMiddleware.cs
│   │   │   │   ├── JwtMiddleware.cs
│   │   │   │   └── RateLimitingMiddleware.cs
│   │   │   ├── Startup.cs
│   │   │   ├── Program.cs
│   │   │   └── appsettings.json
│   │   │
│   │   ├── eSignature.Application/                 # Business Logic & Use Cases (CQRS)
│   │   │   ├── Commands/
│   │   │   │   ├── Documents/
│   │   │   │   │   ├── UploadDocumentCommand.cs
│   │   │   │   │   ├── SendForSignatureCommand.cs
│   │   │   │   │   └── DeleteDocumentCommand.cs
│   │   │   │   ├── Recipients/
│   │   │   │   │   ├── AddRecipientCommand.cs
│   │   │   │   │   └── UpdateRecipientStatusCommand.cs
│   │   │   │   ├── Signatures/
│   │   │   │   │   ├── SignDocumentCommand.cs
│   │   │   │   │   └── RejectDocumentCommand.cs
│   │   │   │   └── Templates/
│   │   │   │       └── CreateTemplateCommand.cs
│   │   │   │
│   │   │   ├── Queries/
│   │   │   │   ├── Documents/
│   │   │   │   │   ├── GetDocumentQuery.cs
│   │   │   │   │   ├── GetDocumentsQuery.cs
│   │   │   │   │   └── GetDocumentStatusQuery.cs
│   │   │   │   ├── Recipients/
│   │   │   │   │   ├── GetRecipientsQuery.cs
│   │   │   │   │   └── GetRecipientStatusQuery.cs
│   │   │   │   └── Audit/
│   │   │   │       └── GetAuditLogQuery.cs
│   │   │   │
│   │   │   ├── Handlers/
│   │   │   │   ├── Commands/
│   │   │   │   └── Queries/
│   │   │   │
│   │   │   ├── DTOs/
│   │   │   │   ├── DocumentDto.cs
│   │   │   │   ├── RecipientDto.cs
│   │   │   │   ├── SignatureFieldDto.cs
│   │   │   │   └── AuditLogDto.cs
│   │   │   │
│   │   │   ├── Mappings/
│   │   │   │   └── MappingProfile.cs
│   │   │   │
│   │   │   ├── Validators/
│   │   │   │   ├── UploadDocumentValidator.cs
│   │   │   │   ├── AddRecipientValidator.cs
│   │   │   │   └── SignDocumentValidator.cs
│   │   │   │
│   │   │   └── Interfaces/
│   │   │       ├── IDocumentService.cs
│   │   │       ├── IRecipientService.cs
│   │   │       ├── ISignatureService.cs
│   │   │       └── IAuditService.cs
│   │   │
│   │   ├── eSignature.Domain/                      # Domain Models & Business Rules
│   │   │   ├── Entities/
│   │   │   │   ├── User.cs
│   │   │   │   ├── Organization.cs
│   │   │   │   ├── Document.cs
│   │   │   │   ├── DocumentRecipient.cs
│   │   │   │   ├── DocumentSignatureField.cs
│   │   │   │   ├── RecipientSignature.cs
│   │   │   │   ├── DocumentWorkflow.cs
│   │   │   │   ├── AuditLog.cs
│   │   │   │   └── DocumentTemplate.cs
│   │   │   │
│   │   │   ├── ValueObjects/
│   │   │   │   ├── Email.cs
│   │   │   │   ├── PhoneNumber.cs
│   │   │   │   ├── AccessToken.cs
│   │   │   │   └── SignatureData.cs
│   │   │   │
│   │   │   ├── Enums/
│   │   │   │   ├── DocumentStatus.cs
│   │   │   │   ├── RecipientRole.cs
│   │   │   │   ├── RecipientStatus.cs
│   │   │   │   ├── SignatureFieldType.cs
│   │   │   │   ├── SignatureType.cs
│   │   │   │   └── WorkflowType.cs
│   │   │   │
│   │   │   ├── Interfaces/
│   │   │   │   └── IAggregateRoot.cs
│   │   │   │
│   │   │   └── Events/
│   │   │       ├── DocumentCreatedEvent.cs
│   │   │       ├── DocumentSignedEvent.cs
│   │   │       └── DocumentCompletedEvent.cs
│   │   │
│   │   ├── eSignature.Infrastructure/              # External Services & Data Access
│   │   │   ├── Persistence/
│   │   │   │   ├── ESignatureDbContext.cs
│   │   │   │   ├── Repositories/
│   │   │   │   │   ├── DocumentRepository.cs
│   │   │   │   │   ├── RecipientRepository.cs
│   │   │   │   │   ├── SignatureRepository.cs
│   │   │   │   │   ├── AuditRepository.cs
│   │   │   │   │   └── GenericRepository.cs
│   │   │   │   ├── UnitOfWork/
│   │   │   │   │   └── UnitOfWork.cs
│   │   │   │   └── Migrations/
│   │   │   │       ├── InitialCreate.cs
│   │   │   │       └── AddAuditLogging.cs
│   │   │   │
│   │   │   ├── Services/
│   │   │   │   ├── PdfService.cs                   # PDF processing with iText7
│   │   │   │   ├── StorageService.cs               # Azure Blob Storage
│   │   │   │   ├── NotificationService.cs          # SendGrid integration
│   │   │   │   ├── AuthenticationService.cs        # Azure AD B2C
│   │   │   │   ├── CacheService.cs                 # Redis caching
│   │   │   │   ├── EncryptionService.cs            # AES encryption
│   │   │   │   ├── GeoLocationService.cs           # IP geolocation
│   │   │   │   └── WebhookService.cs               # Webhook management
│   │   │   │
│   │   │   ├── ExternalServices/
│   │   │   │   ├── SendGridEmailService.cs
│   │   │   │   ├── TwilioSmsService.cs
│   │   │   │   └── AzureKeyVaultService.cs
│   │   │   │
│   │   │   └── Configurations/
│   │   │       ├── JwtSettings.cs
│   │   │       ├── AzureSettings.cs
│   │   │       └── ApplicationSettings.cs
│   │   │
│   │   └── eSignature.Shared/                      # Shared Utilities & Common Code
│   │       ├── Constants/
│   │       │   ├── ErrorCodes.cs
│   │       │   ├── ValidationMessages.cs
│   │       │   └── BusinessConstants.cs
│   │       ├── Exceptions/
│   │       │   ├── ApplicationException.cs
│   │       │   ├── ValidationException.cs
│   │       │   ├── NotFoundException.cs
│   │       │   └── UnauthorizedAccessException.cs
│   │       ├── Extensions/
│   │       │   ├── StringExtensions.cs
│   │       │   ├── EnumExtensions.cs
│   │       │   └── DateTimeExtensions.cs
│   │       ├── Utilities/
│   │       │   ├── HashUtility.cs
│   │       │   ├── EncryptionUtility.cs
│   │       │   ├── ValidationUtility.cs
│   │       │   └── TokenUtility.cs
│   │       └── Models/
│   │           ├── ApiResponse.cs
│   │           ├── PaginatedResult.cs
│   │           └── PagedRequest.cs
│   │
│   ├── tests/
│   │   ├── eSignature.Tests.Unit/
│   │   │   ├── Application/
│   │   │   │   ├── Commands/
│   │   │   │   └── Queries/
│   │   │   ├── Domain/
│   │   │   └── Infrastructure/
│   │   │
│   │   ├── eSignature.Tests.Integration/
│   │   │   ├── Api/
│   │   │   ├── Database/
│   │   │   └── Services/
│   │   │
│   │   └── eSignature.Tests.E2E/
│   │       ├── DocumentWorkflow.cs
│   │       ├── SigningProcess.cs
│   │       └── AuditLogging.cs
│   │
│   ├── eSignature.sln
│   ├── Dockerfile
│   └── docker-compose.yml
│
├── frontend/
│   ├── public/
│   │   ├── index.html
│   │   └── favicon.ico
│   │
│   ├── src/
│   │   ├── components/
│   │   │   ├── Common/
│   │   │   │   ├── Header.tsx
│   │   │   │   ├── Sidebar.tsx
│   │   │   │   ├── Footer.tsx
│   │   │   │   └── LoadingSpinner.tsx
│   │   │   │
│   │   │   ├── Authentication/
│   │   │   │   ├── LoginPage.tsx
│   │   │   │   ├── LogoutButton.tsx
│   │   │   │   ├── ProtectedRoute.tsx
│   │   │   │   └── MFASetup.tsx
│   │   │   │
│   │   │   ├── Dashboard/
│   │   │   │   ├── DashboardPage.tsx
│   │   │   │   ├── DocumentGrid.tsx
│   │   │   │   ├── StatusFilter.tsx
│   │   │   │   └── QuickActions.tsx
│   │   │   │
│   │   │   ├── Documents/
│   │   │   │   ├── DocumentUpload.tsx
│   │   │   │   ├── DocumentPreview.tsx
│   │   │   │   ├── DocumentDetails.tsx
│   │   │   │   ├── DocumentVersionHistory.tsx
│   │   │   │   └── DocumentActions.tsx
│   │   │   │
│   │   │   ├── Recipients/
│   │   │   │   ├── RecipientForm.tsx
│   │   │   │   ├── RecipientList.tsx
│   │   │   │   ├── RecipientStatus.tsx
│   │   │   │   └── RecipientNotifications.tsx
│   │   │   │
│   │   │   ├── SignatureFields/
│   │   │   │   ├── FieldEditor.tsx
│   │   │   │   ├── FieldCanvas.tsx
│   │   │   │   ├── FieldPalette.tsx
│   │   │   │   └── FieldProperties.tsx
│   │   │   │
│   │   │   ├── Signing/
│   │   │   │   ├── SigningInterface.tsx
│   │   │   │   ├── SignatureCanvas.tsx
│   │   │   │   ├── SignatureTypeSelector.tsx
│   │   │   │   ├── SavedSignatures.tsx
│   │   │   │   └── SignaturePad.tsx
│   │   │   │
│   │   │   ├── Workflows/
│   │   │   │   ├── WorkflowBuilder.tsx
│   │   │   │   ├── WorkflowPreview.tsx
│   │   │   │   ├── WorkflowStatus.tsx
│   │   │   │   └── WorkflowTimeline.tsx
│   │   │   │
│   │   │   ├── Templates/
│   │   │   │   ├── TemplateList.tsx
│   │   │   │   ├── TemplateEditor.tsx
│   │   │   │   └── TemplateGallery.tsx
│   │   │   │
│   │   │   ├── Audit/
│   │   │   │   ├── AuditTrail.tsx
│   │   │   │   ├── AuditCertificate.tsx
│   │   │   │   └── AuditReport.tsx
│   │   │   │
│   │   │   └── Settings/
│   │   │       ├── UserSettings.tsx
│   │   │       ├── OrganizationSettings.tsx
│   │   │       ├── BillingSettings.tsx
│   │   │       └── ApiKeyManagement.tsx
│   │   │
│   │   ├── pages/
│   │   │   ├── HomePage.tsx
│   │   │   ├── DashboardPage.tsx
│   │   │   ├── DocumentPage.tsx
│   │   │   ├── SigningPage.tsx
│   │   │   ├── SettingsPage.tsx
│   │   │   └── NotFoundPage.tsx
│   │   │
│   │   ├── services/
│   │   │   ├── api/
│   │   │   │   ├── client.ts
│   │   │   │   ├── documentService.ts
│   │   │   │   ├── recipientService.ts
│   │   │   │   ├── signatureService.ts
│   │   │   │   ├── workflowService.ts
│   │   │   │   ├── auditService.ts
│   │   │   │   └── authService.ts
│   │   │   │
│   │   │   ├── storage/
│   │   │   │   ├── localStorageService.ts
│   │   │   │   └── sessionStorageService.ts
│   │   │   │
│   │   │   └── utils/
│   │   │       ├── dateUtils.ts
│   │   │       ├── fileUtils.ts
│   │   │       ├── validationUtils.ts
│   │   │       └── encryptionUtils.ts
│   │   │
│   │   ├── store/
│   │   │   ├── slices/
│   │   │   │   ├── authSlice.ts
│   │   │   │   ├── documentsSlice.ts
│   │   │   │   ├── recipientsSlice.ts
│   │   │   │   ├── signaturesSlice.ts
│   │   │   │   ├── workflowSlice.ts
│   │   │   │   └── uiSlice.ts
│   │   │   │
│   │   │   ├── index.ts
│   │   │   └── hooks.ts
│   │   │
│   │   ├── hooks/
│   │   │   ├── useAuth.ts
│   │   │   ├── useDocument.ts
│   │   │   ├── useSignature.ts
│   │   │   ├── useApi.ts
│   │   │   └── useLocalStorage.ts
│   │   │
│   │   ├── types/
│   │   │   ├── document.ts
│   │   │   ├── recipient.ts
│   │   │   ├── signature.ts
│   │   │   ├── workflow.ts
│   │   │   ├── audit.ts
│   │   │   └── api.ts
│   │   │
│   │   ├── styles/
│   │   │   ├── index.css
│   │   │   ├── variables.css
│   │   │   ├── components.css
│   │   │   └── responsive.css
│   │   │
│   │   ├── config/
│   │   │   ├── apiConfig.ts
│   │   │   ├── authConfig.ts
│   │   │   └── appConfig.ts
│   │   │
│   │   ├── App.tsx
│   │   └── index.tsx
│   │
│   ├── package.json
│   ├── tsconfig.json
│   ├── .env.example
│   └── Dockerfile
│
├── infrastructure/
│   ├── terraform/
│   │   ├── main.tf
│   │   ├── variables.tf
│   │   ├── outputs.tf
│   │   ├── azure-app-service.tf
│   │   ├── azure-sql-database.tf
│   │   ├── azure-blob-storage.tf
│   │   ├── azure-key-vault.tf
│   │   ├── azure-service-bus.tf
│   │   └── azure-monitoring.tf
│   │
│   ├── kubernetes/
│   │   ├── namespaces.yml
│   │   ├── deployments/
│   │   │   ├── api-deployment.yml
│   │   │   └── worker-deployment.yml
│   │   ├── services/
│   │   │   ├── api-service.yml
│   │   │   └── worker-service.yml
│   │   ├── configmaps/
│   │   │   └── app-config.yml
│   │   ├── secrets/
│   │   │   └── app-secrets.yml
│   │   ├── ingress/
│   │   │   └── main-ingress.yml
│   │   └── helm/
│   │       ├── Chart.yaml
│   │       ├── values.yaml
│   │       └── templates/
│   │
│   └── docker/
│       ├── Dockerfile.api
│       ├── Dockerfile.worker
│       └── docker-compose.yml
│
├── .github/
│   ├── workflows/
│   │   ├── ci-backend.yml
│   │   ├── ci-frontend.yml
│   │   ├── cd-dev.yml
│   │   ├── cd-staging.yml
│   │   ├── cd-production.yml
│   │   └── security-scan.yml
│   │
│   └── pull_request_template.md
│
├── docs/
│   ├── ARCHITECTURE.md
│   ├── DATABASE_SCHEMA.md
│   ├── API_SPECIFICATION.md
│   ├── PROJECT_STRUCTURE.md
│   ├── DEVELOPMENT_GUIDE.md
│   ├── DEPLOYMENT_GUIDE.md
│   ├── SECURITY_GUIDE.md
│   ├── TESTING_GUIDE.md
│   ├── TROUBLESHOOTING.md
│   └── CONTRIBUTING.md
│
├── scripts/
│   ├── setup-dev-environment.sh
│   ├── run-database-migrations.sh
│   ├── seed-test-data.sh
│   ├── generate-jwt-tokens.sh
│   └── deploy-to-azure.sh
│
├── README.md
├── LICENSE
└── .gitignore
```

---

## Key Structure Decisions

### 1. **Clean Architecture Pattern**
- Separation of concerns into layers
- Dependencies point inward
- Easy to test and maintain

### 2. **CQRS Pattern**
- Commands for state changes
- Queries for data retrieval
- Clearer intent and easier to optimize

### 3. **Repository Pattern**
- Data access abstraction
- Easy to mock for testing
- Consistent data access interface

### 4. **Feature-Based Organization (Frontend)**
- Organized by feature, not layer
- Easier to locate and modify features
- Better code organization for large teams

### 5. **Infrastructure as Code**
- Terraform for cloud infrastructure
- Kubernetes for orchestration
- Version controlled and repeatable

---

**Project Structure Version**: 1.0  
**Last Updated**: 2025-06-05
