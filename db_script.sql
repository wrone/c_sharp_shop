-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema shop
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema shop
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `shop` DEFAULT CHARACTER SET utf8 ;
USE `shop` ;

-- -----------------------------------------------------
-- Table `shop`.`Users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `shop`.`Users` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NULL,
  `Lastname` VARCHAR(45) NULL,
  `Login` VARCHAR(45) NULL,
  `Password` VARCHAR(45) NULL,
  `Email` VARCHAR(45) NULL,
  `Phone` VARCHAR(45) NULL,
  `Role` VARCHAR(45) NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `shop`.`Addresses`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `shop`.`Addresses` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NULL,
  `Lastname` VARCHAR(45) NULL,
  `Phone` VARCHAR(45) NULL,
  `Email` VARCHAR(45) NULL,
  `Address` VARCHAR(45) NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `shop`.`Shipping_methods`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `shop`.`Shipping_methods` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NULL,
  `Price` VARCHAR(45) NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `shop`.`Payments`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `shop`.`Payments` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `Date` DATE NULL,
  `CardNumber` VARCHAR(45) NULL,
  `CardHoldersName` VARCHAR(45) NULL,
  `CardHoldersLastname` VARCHAR(45) NULL,
  `ExpDate` DATE NULL,
  `Amount` DECIMAL(6,2) NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `shop`.`Orders`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `shop`.`Orders` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `Users_ID` INT NOT NULL,
  `Addresses_ID` INT NOT NULL,
  `Shipping_methods_ID` INT NOT NULL,
  `Payments_ID` INT NOT NULL,
  `Date` DATE NULL,
  PRIMARY KEY (`ID`),
  INDEX `fk_Orders_Users_idx` (`Users_ID` ASC),
  INDEX `fk_Orders_Addresses1_idx` (`Addresses_ID` ASC),
  INDEX `fk_Orders_Shipping_methods1_idx` (`Shipping_methods_ID` ASC),
  INDEX `fk_Orders_Payments1_idx` (`Payments_ID` ASC),
  CONSTRAINT `fk_Orders_Users`
    FOREIGN KEY (`Users_ID`)
    REFERENCES `shop`.`Users` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Orders_Addresses1`
    FOREIGN KEY (`Addresses_ID`)
    REFERENCES `shop`.`Addresses` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Orders_Shipping_methods1`
    FOREIGN KEY (`Shipping_methods_ID`)
    REFERENCES `shop`.`Shipping_methods` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Orders_Payments1`
    FOREIGN KEY (`Payments_ID`)
    REFERENCES `shop`.`Payments` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `shop`.`Products`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `shop`.`Products` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NULL,
  `Description` VARCHAR(45) NULL,
  `Release_date` DATE NULL,
  `End_date` DATE NULL,
  `Quantity` INT NULL,
  `Price` DECIMAL(6,2) NULL,
  `Category` VARCHAR(45) NULL,
  `Manufacturer` VARCHAR(45) NULL,
  `Picture` VARCHAR(45) NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `shop`.`Order_items`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `shop`.`Order_items` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `Orders_ID` INT NOT NULL,
  `Products_ID` INT NOT NULL,
  `Quantity` INT NULL,
  PRIMARY KEY (`ID`),
  INDEX `fk_Order_items_Orders1_idx` (`Orders_ID` ASC),
  INDEX `fk_Order_items_Products1_idx` (`Products_ID` ASC),
  CONSTRAINT `fk_Order_items_Orders1`
    FOREIGN KEY (`Orders_ID`)
    REFERENCES `shop`.`Orders` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Order_items_Products1`
    FOREIGN KEY (`Products_ID`)
    REFERENCES `shop`.`Products` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
