# EMS API

## Technical Notes
- Uses CQRS Pattern
- Uses MediatR Pattern to keep controller and logic separate
- Uses SqlLite for storing data and EF Core is backend 
- All of the classes (Except for Data Layer) has tests for verifying the logic.

## Testing
- Can be done using Swagger UI which spins up when the application is ran
- Has a `Ems.Api.postman_collection.json` which has all of the request setup for testing purposes.

