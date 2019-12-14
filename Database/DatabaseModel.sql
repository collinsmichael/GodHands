SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';


-- -----------------------------------------------------
-- Table `REC`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `REC` ;

CREATE  TABLE IF NOT EXISTS `REC` (
  `ID` INT NOT NULL ,
  `PATH` VARCHAR(45) NULL ,
  `NAME` VARCHAR(45) NULL ,
  `TYPE` VARCHAR(45) NULL ,
  `LBA` INT NULL ,
  `LEN` INT NULL ,
  PRIMARY KEY (`ID`) ,
  UNIQUE INDEX `ID_UNIQUE` (`ID` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ZND_MPD_LIST`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ZND_MPD_LIST` ;

CREATE  TABLE IF NOT EXISTS `ZND_MPD_LIST` (
  `ID` INT NOT NULL ,
  `ID_REC` INT NULL ,
  `LBA_ZND` INT NULL ,
  `INDEX` INT NULL ,
  `LBA_MPD` INT NULL ,
  `LEN_MPD` INT NULL ,
  PRIMARY KEY (`ID`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ZND_ZUD_LIST`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ZND_ZUD_LIST` ;

CREATE  TABLE IF NOT EXISTS `ZND_ZUD_LIST` (
  `ID` INT NOT NULL ,
  `ID_REC` INT NULL ,
  `LBA_ZND` INT NULL ,
  `INDEX` INT NULL ,
  `LBA_ZUD` INT NULL ,
  `LEN_ZUD` INT NULL ,
  PRIMARY KEY (`ID`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `EQUIP_DATA`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `EQUIP_DATA` ;

CREATE  TABLE IF NOT EXISTS `EQUIP_DATA` (
  `ID` INT NOT NULL ,
  `ID_REC` INT NULL ,
  `LBA_REC` INT NULL ,
  `OFFSET` INT NULL ,
  `LENGTH` INT NULL ,
  `TYPE` VARCHAR(45) NULL ,
  `ITEMNAME_LIST` INT NULL ,
  `ITEM_LIST` INT NULL ,
  `WEP_FILE` INT NULL ,
  `ITEM_CATEGORY` INT NULL ,
  `STR_STAT` INT NULL ,
  `INT_STAT` INT NULL ,
  `AGL_STAT` INT NULL ,
  `CUR_DP` INT NULL ,
  `MAX_DP` INT NULL ,
  `CUR_PP` INT NULL ,
  `MAX_PP` INT NULL ,
  `DMG_TYPE` INT NULL ,
  `STAT_COST` INT NULL ,
  `COST_VALUE` INT NULL ,
  `MATERIAL_ID` INT NULL ,
  `NUM_GEMS` INT NULL ,
  `GEM_EFFECTS` INT NULL ,
  `INVENTORY_INDEX` INT NULL ,
  `RANGE_X` INT NULL ,
  `RANGE_Y` INT NULL ,
  `RANGE_Z` INT NULL ,
  `TARGET_SHAPE` INT NULL ,
  `TARGET_ANGLE` INT NULL ,
  `TYPE_BLUNT` INT NULL ,
  `TYPE_EDGED` INT NULL ,
  `TYPE_PIERCING` INT NULL ,
  `CLASS_EVIL` INT NULL ,
  `CLASS_HUMAN` INT NULL ,
  `CLASS_BEAST` INT NULL ,
  `CLASS_UNDEAD` INT NULL ,
  `CLASS_PHANTOM` INT NULL ,
  `CLASS_DRAGON` INT NULL ,
  `AFFINITY_PHYSICAL` INT NULL ,
  `AFFINITY_EARTH` INT NULL ,
  `AFFINITY_AIR` INT NULL ,
  `AFFINITY_FIRE` INT NULL ,
  `AFFINITY_WATER` INT NULL ,
  `AFFINITY_LIGHT` INT NULL ,
  `AFFINITY_DARK` INT NULL ,
  PRIMARY KEY (`ID`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `WEAPON`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `WEAPON` ;

CREATE  TABLE IF NOT EXISTS `WEAPON` (
  `ID` INT NOT NULL ,
  `ID_REC` INT NULL ,
  `LBA_REC` INT NULL ,
  `OFFSET` INT NULL ,
  `LENGTH` INT NULL ,
  `NAME` VARCHAR(45) NULL ,
  `MATERIAL_ID` INT NULL ,
  `DROP_CHANCE` INT NULL ,
  `ID_BLADE` INT NULL ,
  `ID_GRIP` INT NULL ,
  `ID_GEM1` INT NULL ,
  `ID_GEM2` INT NULL ,
  `ID_GEM3` INT NULL ,
  PRIMARY KEY (`ID`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `SHIELD`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `SHIELD` ;

CREATE  TABLE IF NOT EXISTS `SHIELD` (
  `ID` INT NOT NULL ,
  `ID_REC` INT NULL ,
  `LBA_REC` INT NULL ,
  `OFFSET` INT NULL ,
  `LENGTH` INT NULL ,
  `MATERIAL_ID` INT NULL ,
  `DROP_CHANCE` INT NULL ,
  `ID_SHIELD` INT NULL ,
  `ID_GEM1` INT NULL ,
  `ID_GEM2` INT NULL ,
  `ID_GEM3` INT NULL ,
  PRIMARY KEY (`ID`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ARMOUR`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ARMOUR` ;

CREATE  TABLE IF NOT EXISTS `ARMOUR` (
  `ID` INT NOT NULL ,
  `ID_REC` INT NULL ,
  `LBA_REC` INT NULL ,
  `OFFSET` INT NULL ,
  `LENGTH` INT NULL ,
  `MATERIAL_ID` INT NULL ,
  `DROP_CHANCE` INT NULL ,
  `ID_ARMOUR` INT NULL ,
  PRIMARY KEY (`ID`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ACCESSORY`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ACCESSORY` ;

CREATE  TABLE IF NOT EXISTS `ACCESSORY` (
  `ID` INT NOT NULL ,
  `ID_REC` INT NULL ,
  `LBA_REC` INT NULL ,
  `OFFSET` INT NULL ,
  `LENGTH` INT NULL ,
  `ID_ACCESSORY` INT NULL ,
  `DROP_CHANCE` INT NULL ,
  PRIMARY KEY (`ID`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ZND_NPC`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ZND_NPC` ;

CREATE  TABLE IF NOT EXISTS `ZND_NPC` (
  `ID` INT NOT NULL ,
  `ID_REC` INT NULL ,
  `LBA_REC` INT NULL ,
  `OFFSET` INT NULL ,
  `LENGTH` INT NULL ,
  `NPC_INDEX` INT NULL ,
  `ID_ZUD` INT NULL ,
  `NAME` VARCHAR(45) NULL ,
  `MODEL3D_EFFECTS` INT NULL ,
  `HP_STAT` INT NULL ,
  `MP_STAT` INT NULL ,
  `STR_STAT` INT NULL ,
  `INT_STAT` INT NULL ,
  `AGL_STAT` INT NULL ,
  `SPD_CARRY` INT NULL ,
  `SPD_RUN` INT NULL ,
  `ID_WEAPON` INT NULL ,
  `ID_SHIELD` INT NULL ,
  `ID_ACCESSORY` INT NULL ,
  `ID_BODYPART_1` INT NULL ,
  `ID_BODYPART_2` INT NULL ,
  `ID_BODYPART_3` INT NULL ,
  `ID_BODYPART_4` INT NULL ,
  `ID_BODYPART_5` INT NULL ,
  `ID_BODYPART_6` INT NULL ,
  PRIMARY KEY (`ID`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `BODYPART`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `BODYPART` ;

CREATE  TABLE IF NOT EXISTS `BODYPART` (
  `ID` INT NOT NULL ,
  `ID_REC` INT NULL ,
  `LBA_REC` INT NULL ,
  `OFFSET` INT NULL ,
  `LENGTH` INT NULL ,
  `ID_NPC` INT NULL ,
  `MPD_NPC_ID` INT NULL ,
  `ID_ARMOUR` INT NULL ,
  `HP_STAT` INT NULL ,
  `AGL_BONUS` INT NULL ,
  `CHAIN_EVADE` INT NULL ,
  `TYPE_BLUNT` INT NULL ,
  `TYPE_EDGED` INT NULL ,
  `TYPE_PIERCING` INT NULL ,
  `AFFINITY_PHYSICAL` INT NULL ,
  `AFFINITY_EARTH` INT NULL ,
  `AFFINITY_AIR` INT NULL ,
  `AFFINITY_FIRE` INT NULL ,
  `AFFINITY_WATER` INT NULL ,
  `AFFINITY_LIGHT` INT NULL ,
  `AFFINITY_DARK` INT NULL ,
  `SKILL_LIST_0` INT NULL ,
  `SKILL_NO_0` INT NULL ,
  `SKILL_LIST_1` INT NULL ,
  `SKILL_NO_1` INT NULL ,
  `SKILL_LIST_2` INT NULL ,
  `SKILL_NO_2` INT NULL ,
  `SKILL_LIST_3` INT NULL ,
  `SKILL_NO_3` INT NULL ,
  `DMG_DISTRIB_1` INT NULL ,
  `DMG_DISTRIB_2` INT NULL ,
  `DMG_DISTRIB_3` INT NULL ,
  `DMG_DISTRIB_4` INT NULL ,
  `DMG_DISTRIB_5` INT NULL ,
  `DMG_DISTRIB_6` INT NULL ,
  PRIMARY KEY (`ID`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `TREASURE_CHEST`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `TREASURE_CHEST` ;

CREATE  TABLE IF NOT EXISTS `TREASURE_CHEST` (
  `ID` INT NOT NULL ,
  `ID_REC` INT NULL ,
  `LBA_REC` INT NULL ,
  `OFFSET` INT NULL ,
  `LENGTH` INT NULL ,
  `ID_WEAPON` INT NULL ,
  `ID_BLADE` INT NULL ,
  `ID_GRIP` INT NULL ,
  `ID_SHIELD` INT NULL ,
  `ID_ARMOUR_1` INT NULL ,
  `ID_ARMOUR_2` INT NULL ,
  `ID_ACCESSORY` INT NULL ,
  `ID_GEM` INT NULL ,
  `ITEM_1_NAME` INT NULL ,
  `ITEM_1_QTY` INT NULL ,
  `ITEM_2_NAME` INT NULL ,
  `ITEM_2_QTY` INT NULL ,
  `ITEM_3_NAME` INT NULL ,
  `ITEM_3_QTY` INT NULL ,
  `ITEM_4_NAME` INT NULL ,
  `ITEM_4_QTY` INT NULL ,
  PRIMARY KEY (`ID`) )
ENGINE = InnoDB;



SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
