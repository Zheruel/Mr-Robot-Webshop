drop table WebShopUser;
drop table ProductInfo;
drop table Product;
drop table SubCategory;
drop table Category;
drop table Receipt;
-- Create a new database called 'MrRobotWebshopDB'
-- Connect to the 'master' database to run this snippet
-- USE master
-- GO
-- Create the new database if it does not exist already
-- IF NOT EXISTS (
    -- SELECT name
        -- FROM sys.databases
        -- WHERE name = 'MrRobotWebshopDB'
-- )
-- CREATE DATABASE MrRobotWebshopDB
-- GO

CREATE TABLE Category (
    CategoryID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    CategoryName VARCHAR(255),
);

CREATE TABLE SubCategory (
    SubCategoryID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    SubCategoryName VARCHAR(255),
    CategoryID INT FOREIGN KEY REFERENCES Category(CategoryID)
);

CREATE TABLE WebshopUser (
    WebshopUserID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(255),
    Password VARCHAR(255),
    Salt VARCHAR(255),
    Firstname VARCHAR(255),
    Lastname VARCHAR(255)
);

CREATE TABLE Receipt (
    ReceiptID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Status VARCHAR(255),
    FinalPrice VARCHAR(255)
);

CREATE TABLE Product (
    ProductID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    ProductName VARCHAR(255),
    SubCategoryID INT FOREIGN KEY REFERENCES SubCategory(SubCategoryID)
);

CREATE TABLE ProductInfo (
    ProductInfoID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Price DECIMAL,
    Amount INT,
    ProductID INT FOREIGN KEY REFERENCES Product(ProductID),
    ReceiptID INT FOREIGN KEY REFERENCES Receipt(ReceiptID)
);