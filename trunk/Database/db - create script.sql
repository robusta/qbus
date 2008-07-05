DROP DATABASE IF EXISTS `homeapplication`;

CREATE DATABASE IF NOT EXISTS `homeapplication`
CHARACTER SET latin1;

USE `homeapplication`;

SET autocommit=0;

SET FOREIGN_KEY_CHECKS=0;

#   ----------- SYSTEEMTABELLEN -----------

CREATE TABLE `homeapplication`.`sysobject` (
	`sysobject_id` int(10) UNSIGNED NOT NULL DEFAULT 0,
	`name` varchar(50) NOT NULL DEFAULT '',
	`description` varchar(250),
	`last_id` int(10) UNSIGNED NOT NULL DEFAULT 0,
	`crud` int(10) UNSIGNED,
	PRIMARY KEY(`sysobject_id`)
)
ENGINE=INNODB;

CREATE TABLE `homeapplication`.`sysattribute` (
	`sysattribute_id` int(10) UNSIGNED NOT NULL DEFAULT 0,
	`sysobject_id` int(10) UNSIGNED NOT NULL DEFAULT 0,
	`sequencenumber` int(10) UNSIGNED NOT NULL DEFAULT 0,
	`name` varchar(50) NOT NULL DEFAULT '',
	`description` varchar(250),
	`datatype` varchar(25) NOT NULL DEFAULT '',
	`enumtype` varchar(50),
	`display` varchar(25),
	`required` boolean NOT NULL DEFAULT 0,
	`enabled` boolean NOT NULL DEFAULT 0,
	`uniek` boolean NOT NULL DEFAULT 0,
	`defaultvalue` varchar(250),
	`minvalue` varchar(250),
	`maxvalue` varchar(250),
	`maxlength` int(10) UNSIGNED,
	`crud` int(10) UNSIGNED,
	PRIMARY KEY(`sysattribute_id`),
	CONSTRAINT `FK_sysattribute-sysobject` FOREIGN KEY (`sysobject_id`)
		REFERENCES `sysobject`(`sysobject_id`)
)
ENGINE=INNODB;

CREATE TABLE `homeapplication`.`sysenumeration` (
	`sysenumeration_id` int(10) UNSIGNED NOT NULL DEFAULT 0,
	`enumtype` varchar(50) NOT NULL DEFAULT '',
	`name` varchar(50) NOT NULL DEFAULT '',
	`description` varchar(250),
	`sequencenumber` int(10) UNSIGNED NOT NULL DEFAULT 0,
	PRIMARY KEY(`sysenumeration_id`)
)
ENGINE=INNODB;

CREATE TABLE `homeapplication`.`sysrelation` (
	`sysrelation_id` int(10) UNSIGNED NOT NULL DEFAULT 0,
	`name` varchar(50) NOT NULL DEFAULT '',
	`from_sysobject` varchar(50) NOT NULL DEFAULT '',
	`from_sysattribute` varchar(50) NOT NULL DEFAULT '',
	`to_sysobject` varchar(50) NOT NULL DEFAULT '',
	`to_sysattribute` varchar(50) NOT NULL DEFAULT '',
	`linktype` varchar(25) NOT NULL DEFAULT '',
	`linktable` varchar(50),
	PRIMARY KEY(`sysrelation_id`)
)
ENGINE=INNODB;

CREATE TABLE `homeapplication`.`sysexception` (
  `sysexception_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `exceptiontype` VARCHAR(25),
  `reference` VARCHAR(25),
  `name` VARCHAR(100) NOT NULL DEFAULT '',
  `description` VARCHAR(2500),
  PRIMARY KEY(`sysexception_id`)
)
ENGINE = InnoDB;



#   ----------- DOMOTICATABELLEN -----------


CREATE TABLE `homeapplication`.`qbus_module` (
  `qbus_module_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `moduletype_enumid` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `modulenumber` VARCHAR(10) NOT NULL DEFAULT '',
  `name` VARCHAR(100) NOT NULL DEFAULT '',
  `moduleplace_enumid` INTEGER UNSIGNED,
  PRIMARY KEY(`qbus_module_id`)
)
ENGINE = InnoDB;

CREATE TABLE `homeapplication`.`qbus_output` (
  `qbus_output_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `qbus_module_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `outputnumber` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `address` INTEGER UNSIGNED,
  `subaddress` INTEGER UNSIGNED,
  PRIMARY KEY(`qbus_output_id`),
  CONSTRAINT `FK-qbus_output-qbus_module` FOREIGN KEY `FK-qbus_output-qbus_module` (`qbus_module_id`)
    REFERENCES `qbus_module` (`qbus_module_id`)
    ON DELETE RESTRICT
)
ENGINE = InnoDB;

CREATE TABLE `homeapplication`.`qbus_ruimte` (
  `qbus_ruimte_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `sequencenumber` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `name` VARCHAR(100) NOT NULL DEFAULT '',
  `description` VARCHAR(500),
  PRIMARY KEY(`qbus_ruimte_id`)
)
ENGINE = InnoDB;

CREATE TABLE `homeapplication`.`qbus_output_vwzone` (
  `qbus_output_vwzone_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `qbus_output_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `sequencenumber` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `name` VARCHAR(100) NOT NULL DEFAULT '',
  `comments` VARCHAR(500),
  `night` BOOLEAN,
  `night_temp` DOUBLE,
  `economy` BOOLEAN,
  `economy_temp` DOUBLE,
  `comfort` BOOLEAN,
  `comfort_temp` DOUBLE,
  PRIMARY KEY(`qbus_output_vwzone_id`),
  CONSTRAINT `FK-qbus_output_vwzone-qbus_output` FOREIGN KEY `FK-qbus_output_vwzone-qbus_output` (`qbus_output_id`)
    REFERENCES `qbus_output` (`qbus_output_id`)
)
ENGINE = InnoDB;

CREATE TABLE `homeapplication`.`lnk_qbus_output_vwzone_qbus_ruimte` (
  `qbus_output_vwzone_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `qbus_ruimte_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  CONSTRAINT `FK-qbus_output_vwzone` FOREIGN KEY `FK-qbus_output_vwzone` (`qbus_output_vwzone_id`)
    REFERENCES `qbus_output_vwzone` (`qbus_output_vwzone_id`)
    ON DELETE RESTRICT,
  CONSTRAINT `FK-qbus_ruimte` FOREIGN KEY `FK-qbus_ruimte` (`qbus_ruimte_id`)
    REFERENCES `qbus_ruimte` (`qbus_ruimte_id`)
    ON DELETE RESTRICT
)
ENGINE = InnoDB;

CREATE TABLE `homeapplication`.`logging_qbus_vwzone` (
  `logging_qbus_vwzone_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `qbus_output_vwzone_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `date` DATE NOT NULL DEFAULT 0,
  `time` TIME NOT NULL DEFAULT 0,
  `vwprofile_enumid` INTEGER UNSIGNED,
  `temp_desired` DOUBLE,
  `temp_current` DOUBLE,
  `vwstatus_enumid` INTEGER UNSIGNED,
  PRIMARY KEY(`logging_qbus_vwzone_id`)
)
ENGINE = InnoDB;

CREATE TABLE `homeapplication`.`qbus_codes` (
  `qbus_codes_id` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `codetype_enumid` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `code` VARCHAR(10) NOT NULL DEFAULT '',
  `translation` VARCHAR(100) NOT NULL DEFAULT '',
  PRIMARY KEY(`qbus_codes_id`)
)
ENGINE = InnoDB;





SET FOREIGN_KEY_CHECKS=1;

COMMIT;