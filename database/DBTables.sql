USE DecoCrudDB;

CREATE TABLE Tenant (
    TenantId INT PRIMARY KEY,
    TenantName VARCHAR(100) NOT NULL
);

CREATE TABLE [User] (
    UserID INT PRIMARY KEY,
    UserName VARCHAR(100) NOT NULL,
    TenantId INT,
    FOREIGN KEY (TenantId) REFERENCES Tenant(TenantId)

);


