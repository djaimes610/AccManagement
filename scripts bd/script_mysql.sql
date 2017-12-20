-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema tp2
-- -----------------------------------------------------
SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `tipo_emision_comprobante`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `tipo_emision_comprobante` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `tipo_emision_comprobante` (
  `id_tipemision` INT NOT NULL,
  `nombre_tipemision` VARCHAR(45) NULL,
  PRIMARY KEY (`id_tipemision`))
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `tipo_comprobante`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `tipo_comprobante` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `tipo_comprobante` (
  `id_tipcomp` INT NOT NULL,
  `nombre_tipcomp` VARCHAR(45) NULL,
  PRIMARY KEY (`id_tipcomp`))
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `tipo_orden_pago`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `tipo_orden_pago` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `tipo_orden_pago` (
  `id_tiporden` INT NOT NULL,
  `nombre_tiporden` VARCHAR(45) NULL,
  PRIMARY KEY (`id_tiporden`))
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `orden_pago`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `orden_pago` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `orden_pago` (
  `id_orden_pago` INT NOT NULL,
  `numero_orden` VARCHAR(45) NULL,
  `fecha_orden` DATE NULL,
  `estado_orden` INT NULL,
  `usuario_creacion` VARCHAR(45) NULL,
  `fecha_creacion` DATE NULL,
  `usuario_modificacion` VARCHAR(45) NULL,
  `fecha_modificacion` DATE NULL,
  `tipo_orden_pago_id_tiporden` INT NOT NULL,
  PRIMARY KEY (`id_orden_pago`, `tipo_orden_pago_id_tiporden`),
  INDEX `fk_orden_pago_tipo_orden_pago1_idx` (`tipo_orden_pago_id_tiporden` ASC),
  CONSTRAINT `fk_orden_pago_tipo_orden_pago1`
    FOREIGN KEY (`tipo_orden_pago_id_tiporden`)
    REFERENCES `tipo_orden_pago` (`id_tiporden`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `programacion_pago`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `programacion_pago` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `programacion_pago` (
  `id_progpago` INT NOT NULL,
  `fecha_progpago` DATE NULL,
  `importe_progpago` DECIMAL(19,2) NULL,
  `estado_progpago` INT NULL,
  PRIMARY KEY (`id_progpago`))
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `gcom_exx_proveedor`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `gcom_exx_proveedor` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `gcom_exx_proveedor` (
  `id_gcom` INT NOT NULL,
  `ruc_proveedor` VARCHAR(45) NULL,
  `nombre_proveedor` VARCHAR(200) NULL,
  PRIMARY KEY (`id_gcom`))
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `gcom_exx_ordencompra`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `gcom_exx_ordencompra` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `gcom_exx_ordencompra` (
  `id_orden` INT NOT NULL,
  `nombre_orden` VARCHAR(45) NULL,
  PRIMARY KEY (`id_orden`))
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `comprobante`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `comprobante` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `comprobante` (
  `id_comprobante` INT NOT NULL,
  `numero_comprobante` VARCHAR(45) NULL,
  `fecha_emision` DATE NULL,
  `fecha_vencimiento` DATE NULL,
  `afecto` DECIMAL(19,2) NULL,
  `no_afecto` DECIMAL(19,2) NULL,
  `impuesto` DECIMAL(19,2) NULL,
  `total` DECIMAL(19,2) NULL,
  `direccion_cliente` VARCHAR(200) NULL,
  `direccion_cliente2` VARCHAR(200) NULL,
  `usuario_creacion` VARCHAR(45) NULL,
  `fecha_creacion` DATE NULL,
  `usuario_modificacion` VARCHAR(45) NULL,
  `fecha_modificacion` DATE NULL,
  `tipo_emision_comprobante_id_tipemision` INT NOT NULL,
  `tipo_comprobante_id_tipcomp` INT NOT NULL,
  `orden_pago_id_orden_pago` INT NOT NULL,
  `programacion_pago_id_progpago` INT NOT NULL,
  `gcom_exx_proveedor_id_gcom` INT NOT NULL,
  `gcom_exx_ordencompra_id_orden` INT NOT NULL,
  PRIMARY KEY (`id_comprobante`, `tipo_emision_comprobante_id_tipemision`, `tipo_comprobante_id_tipcomp`, `orden_pago_id_orden_pago`, `programacion_pago_id_progpago`),
  INDEX `fk_comprobante_tipo_emision_comprobante1_idx` (`tipo_emision_comprobante_id_tipemision` ASC),
  INDEX `fk_comprobante_tipo_comprobante1_idx` (`tipo_comprobante_id_tipcomp` ASC),
  INDEX `fk_comprobante_orden_pago1_idx` (`orden_pago_id_orden_pago` ASC),
  INDEX `fk_comprobante_programacion_pago1_idx` (`programacion_pago_id_progpago` ASC),
  INDEX `fk_comprobante_gcom_exx_proveedor1_idx` (`gcom_exx_proveedor_id_gcom` ASC),
  INDEX `fk_comprobante_gcom_exx_ordencompra1_idx` (`gcom_exx_ordencompra_id_orden` ASC),
  CONSTRAINT `fk_comprobante_tipo_emision_comprobante1`
    FOREIGN KEY (`tipo_emision_comprobante_id_tipemision`)
    REFERENCES `tipo_emision_comprobante` (`id_tipemision`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_comprobante_tipo_comprobante1`
    FOREIGN KEY (`tipo_comprobante_id_tipcomp`)
    REFERENCES `tipo_comprobante` (`id_tipcomp`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_comprobante_orden_pago1`
    FOREIGN KEY (`orden_pago_id_orden_pago`)
    REFERENCES `orden_pago` (`id_orden_pago`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_comprobante_programacion_pago1`
    FOREIGN KEY (`programacion_pago_id_progpago`)
    REFERENCES `programacion_pago` (`id_progpago`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_comprobante_gcom_exx_proveedor1`
    FOREIGN KEY (`gcom_exx_proveedor_id_gcom`)
    REFERENCES `gcom_exx_proveedor` (`id_gcom`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_comprobante_gcom_exx_ordencompra1`
    FOREIGN KEY (`gcom_exx_ordencompra_id_orden`)
    REFERENCES `gcom_exx_ordencompra` (`id_orden`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `cronograma_pago`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `cronograma_pago` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `cronograma_pago` (
  `id_cronpago` INT NOT NULL,
  `fecha_cronpago` DATE NULL,
  `programacion_pago_id_progpago` INT NOT NULL,
  PRIMARY KEY (`id_cronpago`, `programacion_pago_id_progpago`),
  INDEX `fk_cronograma_pago_programacion_pago1_idx` (`programacion_pago_id_progpago` ASC),
  CONSTRAINT `fk_cronograma_pago_programacion_pago1`
    FOREIGN KEY (`programacion_pago_id_progpago`)
    REFERENCES `programacion_pago` (`id_progpago`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `control_pago`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `control_pago` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `control_pago` (
  `id_control_pago` INT NOT NULL,
  `fecha_control_pago` DATE NULL,
  `estado_control_pago` INT NULL,
  `cronograma_pago_id_cronpago` INT NOT NULL,
  PRIMARY KEY (`id_control_pago`),
  INDEX `fk_control_pago_cronograma_pago1_idx` (`cronograma_pago_id_cronpago` ASC),
  CONSTRAINT `fk_control_pago_cronograma_pago1`
    FOREIGN KEY (`cronograma_pago_id_cronpago`)
    REFERENCES `cronograma_pago` (`id_cronpago`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `tipo_frec_pago`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `tipo_frec_pago` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `tipo_frec_pago` (
  `id_tipofrecpago` INT NOT NULL,
  `nombre_tipofrecpago` VARCHAR(45) NULL,
  PRIMARY KEY (`id_tipofrecpago`))
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `frec_pago_presupuesto`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `frec_pago_presupuesto` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `frec_pago_presupuesto` (
  `id_frecpago` INT NOT NULL,
  `valor_frecpago` DECIMAL(19,2) NULL,
  `tipo_frec_pago_id_tipofrecpago` INT NOT NULL,
  PRIMARY KEY (`id_frecpago`),
  INDEX `fk_frec_pago_presupuesto_tipo_frec_pago1_idx` (`tipo_frec_pago_id_tipofrecpago` ASC),
  CONSTRAINT `fk_frec_pago_presupuesto_tipo_frec_pago1`
    FOREIGN KEY (`tipo_frec_pago_id_tipofrecpago`)
    REFERENCES `tipo_frec_pago` (`id_tipofrecpago`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `presupuesto_pago`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `presupuesto_pago` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `presupuesto_pago` (
  `id_presupuesto` INT NOT NULL,
  `fecha_presupuesto` DATE NULL,
  `importe_presupuesto` DECIMAL(19,2) NULL,
  `control_pago_id_control_pago` INT NOT NULL,
  `frec_pago_presupuesto_id_frecpago` INT NOT NULL,
  PRIMARY KEY (`id_presupuesto`, `control_pago_id_control_pago`, `frec_pago_presupuesto_id_frecpago`),
  INDEX `fk_presupuesto_pago_control_pago1_idx` (`control_pago_id_control_pago` ASC),
  INDEX `fk_presupuesto_pago_frec_pago_presupuesto1_idx` (`frec_pago_presupuesto_id_frecpago` ASC),
  CONSTRAINT `fk_presupuesto_pago_control_pago1`
    FOREIGN KEY (`control_pago_id_control_pago`)
    REFERENCES `control_pago` (`id_control_pago`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_presupuesto_pago_frec_pago_presupuesto1`
    FOREIGN KEY (`frec_pago_presupuesto_id_frecpago`)
    REFERENCES `frec_pago_presupuesto` (`id_frecpago`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `orden_pago_detalle`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `orden_pago_detalle` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `orden_pago_detalle` (
  `id_op_detalle` INT NOT NULL,
  `descripcion` VARCHAR(45) NULL,
  `importe` DECIMAL(19,2) NULL,
  `orden_pago_id_orden_pago` INT NOT NULL,
  PRIMARY KEY (`id_op_detalle`, `orden_pago_id_orden_pago`),
  INDEX `fk_orden_pago_detalle_orden_pago1_idx` (`orden_pago_id_orden_pago` ASC),
  CONSTRAINT `fk_orden_pago_detalle_orden_pago1`
    FOREIGN KEY (`orden_pago_id_orden_pago`)
    REFERENCES `orden_pago` (`id_orden_pago`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `factura`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `factura` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `factura` (
  `id_factura` INT NOT NULL,
  `ruc` VARCHAR(45) NULL,
  `razon_social` VARCHAR(200) NULL,
  `ubigeo` VARCHAR(45) NULL,
  `comprobante_id_comprobante` INT NOT NULL,
  PRIMARY KEY (`id_factura`),
  INDEX `fk_factura_comprobante1_idx` (`comprobante_id_comprobante` ASC),
  CONSTRAINT `fk_factura_comprobante1`
    FOREIGN KEY (`comprobante_id_comprobante`)
    REFERENCES `comprobante` (`id_comprobante`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `tipo_documento_identidad`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `tipo_documento_identidad` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `tipo_documento_identidad` (
  `id_tipdoc` INT NOT NULL,
  `nombre_tipdoc` VARCHAR(45) NULL,
  PRIMARY KEY (`id_tipdoc`))
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `boleta`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `boleta` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `boleta` (
  `id_boleta` INT NOT NULL,
  `nombre_cliente` VARCHAR(200) NULL,
  `numero_documento` VARCHAR(45) NULL,
  `comprobante_id_comprobante` INT NOT NULL,
  `tipo_documento_identidad_id_tipdoc` INT NOT NULL,
  PRIMARY KEY (`id_boleta`, `tipo_documento_identidad_id_tipdoc`),
  INDEX `fk_boleta_comprobante1_idx` (`comprobante_id_comprobante` ASC),
  INDEX `fk_boleta_tipo_documento_identidad1_idx` (`tipo_documento_identidad_id_tipdoc` ASC),
  CONSTRAINT `fk_boleta_comprobante1`
    FOREIGN KEY (`comprobante_id_comprobante`)
    REFERENCES `comprobante` (`id_comprobante`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_boleta_tipo_documento_identidad1`
    FOREIGN KEY (`tipo_documento_identidad_id_tipdoc`)
    REFERENCES `tipo_documento_identidad` (`id_tipdoc`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `boleta_detalle`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `boleta_detalle` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `boleta_detalle` (
  `id_bol_detalle` INT NOT NULL,
  `codigo_producto` VARCHAR(45) NULL,
  `descripcion_producto` VARCHAR(200) NULL,
  `unidad_producto` VARCHAR(45) NULL,
  `cantidad_producto` DECIMAL(19,2) NULL,
  `afecto` DECIMAL(19,2) NULL,
  `no_afecto` DECIMAL(19,2) NULL,
  `impuesto` DECIMAL(19,2) NULL,
  `derecho_emision` DECIMAL(19,2) NULL,
  `total` DECIMAL(19,2) NULL,
  `boleta_id_boleta` INT NOT NULL,
  PRIMARY KEY (`id_bol_detalle`, `boleta_id_boleta`),
  INDEX `fk_boleta_detalle_boleta_idx` (`boleta_id_boleta` ASC),
  CONSTRAINT `fk_boleta_detalle_boleta`
    FOREIGN KEY (`boleta_id_boleta`)
    REFERENCES `boleta` (`id_boleta`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `factura_detalle`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `factura_detalle` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `factura_detalle` (
  `id_fac_detalle` INT NOT NULL,
  `codigo_producto` VARCHAR(45) NULL,
  `descripcion_producto` VARCHAR(200) NULL,
  `unidad_producto` VARCHAR(45) NULL,
  `cantidad_producto` DECIMAL(19,2) NULL,
  `afecto` DECIMAL(19,2) NULL,
  `no_afecto` DECIMAL(19,2) NULL,
  `impuesto` DECIMAL(19,2) NULL,
  `derecho_emision` DECIMAL(19,2) NULL,
  `total` DECIMAL(19,2) NULL,
  `factura_id_factura` INT NOT NULL,
  PRIMARY KEY (`id_fac_detalle`, `factura_id_factura`),
  INDEX `fk_factura_detalle_factura1_idx` (`factura_id_factura` ASC),
  CONSTRAINT `fk_factura_detalle_factura1`
    FOREIGN KEY (`factura_id_factura`)
    REFERENCES `factura` (`id_factura`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `moneda`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `moneda` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `moneda` (
  `id_moneda` INT NOT NULL,
  `nombre_moneda` VARCHAR(45) NULL,
  `signo_monetario` VARCHAR(45) NULL,
  PRIMARY KEY (`id_moneda`))
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `pago`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `pago` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `pago` (
  `id_pago` INT NOT NULL,
  `fecha_pago` DATE NULL,
  `estado_pago` INT NULL,
  `orden_pago_id_orden_pago` INT NOT NULL,
  `moneda_id_moneda` INT NOT NULL,
  PRIMARY KEY (`id_pago`, `orden_pago_id_orden_pago`),
  INDEX `fk_pago_orden_pago1_idx` (`orden_pago_id_orden_pago` ASC),
  INDEX `fk_pago_moneda1_idx` (`moneda_id_moneda` ASC),
  CONSTRAINT `fk_pago_orden_pago1`
    FOREIGN KEY (`orden_pago_id_orden_pago`)
    REFERENCES `orden_pago` (`id_orden_pago`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_pago_moneda1`
    FOREIGN KEY (`moneda_id_moneda`)
    REFERENCES `moneda` (`id_moneda`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `cheque`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `cheque` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `cheque` (
  `id_cheque` INT NOT NULL,
  `numero_cheque` VARCHAR(45) NULL,
  `fecha_emision` DATE NULL,
  `cantidad` INT NULL,
  `total` DECIMAL(19,2) NULL,
  `pago_id_pago` INT NOT NULL,
  PRIMARY KEY (`id_cheque`),
  INDEX `fk_cheque_pago1_idx` (`pago_id_pago` ASC),
  CONSTRAINT `fk_cheque_pago1`
    FOREIGN KEY (`pago_id_pago`)
    REFERENCES `pago` (`id_pago`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `transferencia`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `transferencia` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `transferencia` (
  `id_transferencia` INT NOT NULL,
  `numero_transferencia` VARCHAR(45) NULL,
  `fecha_transferencia` DATE NULL,
  `cantidad` INT NULL,
  `total` DECIMAL(19,5) NULL,
  `pago_id_pago` INT NOT NULL,
  PRIMARY KEY (`id_transferencia`),
  INDEX `fk_transferencia_pago1_idx` (`pago_id_pago` ASC),
  CONSTRAINT `fk_transferencia_pago1`
    FOREIGN KEY (`pago_id_pago`)
    REFERENCES `pago` (`id_pago`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `lote_transferencia`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `lote_transferencia` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `lote_transferencia` (
  `id_lote` INT NOT NULL,
  `numero_lote` VARCHAR(45) NULL,
  `fecha_lote` DATE NULL,
  `estado` INT NULL,
  `transferencia_id_transferencia` INT NOT NULL,
  PRIMARY KEY (`id_lote`, `transferencia_id_transferencia`),
  INDEX `fk_lote_transferencia_transferencia1_idx` (`transferencia_id_transferencia` ASC),
  CONSTRAINT `fk_lote_transferencia_transferencia1`
    FOREIGN KEY (`transferencia_id_transferencia`)
    REFERENCES `transferencia` (`id_transferencia`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `cheque_detalle`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `cheque_detalle` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `cheque_detalle` (
  `id_cheqdet` INT NOT NULL,
  `numero_correlativo` VARCHAR(45) NULL,
  `descripcion` VARCHAR(200) NULL,
  `importe` DECIMAL(19,2) NULL,
  `notificar_cheqdet` TINYINT NULL,
  `cheque_id_cheque` INT NOT NULL,
  PRIMARY KEY (`id_cheqdet`, `cheque_id_cheque`),
  INDEX `fk_cheque_detalle_cheque1_idx` (`cheque_id_cheque` ASC),
  CONSTRAINT `fk_cheque_detalle_cheque1`
    FOREIGN KEY (`cheque_id_cheque`)
    REFERENCES `cheque` (`id_cheque`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `banco`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `banco` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `banco` (
  `id_banco` INT NOT NULL,
  `nombre` VARCHAR(45) NULL,
  PRIMARY KEY (`id_banco`))
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `ctabancaria_proveedor`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `ctabancaria_proveedor` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `ctabancaria_proveedor` (
  `id_ctabancaria` INT NOT NULL,
  `numero_cuenta` VARCHAR(200) NULL,
  `estado_cuenta` TINYINT NULL,
  `banco_id_banco` INT NOT NULL,
  `moneda_id_moneda` INT NOT NULL,
  `gcom_exx_proveedor_id_gcom` INT NOT NULL,
  PRIMARY KEY (`id_ctabancaria`),
  INDEX `fk_ctabancaria_proveedor_banco1_idx` (`banco_id_banco` ASC),
  INDEX `fk_ctabancaria_proveedor_moneda1_idx` (`moneda_id_moneda` ASC),
  INDEX `fk_ctabancaria_proveedor_gcom_exx_proveedor1_idx` (`gcom_exx_proveedor_id_gcom` ASC),
  CONSTRAINT `fk_ctabancaria_proveedor_banco1`
    FOREIGN KEY (`banco_id_banco`)
    REFERENCES `banco` (`id_banco`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_ctabancaria_proveedor_moneda1`
    FOREIGN KEY (`moneda_id_moneda`)
    REFERENCES `moneda` (`id_moneda`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_ctabancaria_proveedor_gcom_exx_proveedor1`
    FOREIGN KEY (`gcom_exx_proveedor_id_gcom`)
    REFERENCES `gcom_exx_proveedor` (`id_gcom`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `transferencia_detalle`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `transferencia_detalle` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `transferencia_detalle` (
  `id_transfdet` INT NOT NULL,
  `importe` DECIMAL(19,2) NULL,
  `notificar_transfdet` TINYINT NULL,
  `estado` TINYINT NULL,
  `transferencia_id_transferencia` INT NOT NULL,
  `ctabancaria_proveedor_id_ctabancaria` INT NOT NULL,
  PRIMARY KEY (`id_transfdet`, `transferencia_id_transferencia`, `ctabancaria_proveedor_id_ctabancaria`),
  INDEX `fk_transferencia_detalle_transferencia1_idx` (`transferencia_id_transferencia` ASC),
  INDEX `fk_transferencia_detalle_ctabancaria_proveedor1_idx` (`ctabancaria_proveedor_id_ctabancaria` ASC),
  CONSTRAINT `fk_transferencia_detalle_transferencia1`
    FOREIGN KEY (`transferencia_id_transferencia`)
    REFERENCES `transferencia` (`id_transferencia`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_transferencia_detalle_ctabancaria_proveedor1`
    FOREIGN KEY (`ctabancaria_proveedor_id_ctabancaria`)
    REFERENCES `ctabancaria_proveedor` (`id_ctabancaria`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `tipo_cuenta_bancaria`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `tipo_cuenta_bancaria` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `tipo_cuenta_bancaria` (
  `id_tipo` INT NOT NULL,
  `numero_cuenta` VARCHAR(200) NULL,
  `estado_cuenta` TINYINT NULL,
  PRIMARY KEY (`id_tipo`))
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `tgsi_e01_empresa`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `tgsi_e01_empresa` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `tgsi_e01_empresa` (
  `id_empresa` INT NOT NULL,
  `ruc_empresa` VARCHAR(45) NULL,
  `nombre_empresa` VARCHAR(200) NULL,
  PRIMARY KEY (`id_empresa`))
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `cuentabancaria_empresa`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `cuentabancaria_empresa` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `cuentabancaria_empresa` (
  `id_cuenta` INT NOT NULL,
  `numero_cuenta` VARCHAR(200) NULL,
  `banco_id_banco` INT NOT NULL,
  `transferencia_id_transferencia` INT NOT NULL,
  `moneda_id_moneda` INT NOT NULL,
  `tipo_cuenta_bancaria_id_tipo` INT NOT NULL,
  `tgsi_e01_empresa_id_empresa` INT NOT NULL,
  PRIMARY KEY (`id_cuenta`, `transferencia_id_transferencia`, `moneda_id_moneda`),
  INDEX `fk_cuentabancaria_empresa_banco1_idx` (`banco_id_banco` ASC),
  INDEX `fk_cuentabancaria_empresa_transferencia1_idx` (`transferencia_id_transferencia` ASC),
  INDEX `fk_cuentabancaria_empresa_moneda1_idx` (`moneda_id_moneda` ASC),
  INDEX `fk_cuentabancaria_empresa_tipo_cuenta_bancaria1_idx` (`tipo_cuenta_bancaria_id_tipo` ASC),
  INDEX `fk_cuentabancaria_empresa_tgsi_e01_empresa1_idx` (`tgsi_e01_empresa_id_empresa` ASC),
  CONSTRAINT `fk_cuentabancaria_empresa_banco1`
    FOREIGN KEY (`banco_id_banco`)
    REFERENCES `banco` (`id_banco`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_cuentabancaria_empresa_transferencia1`
    FOREIGN KEY (`transferencia_id_transferencia`)
    REFERENCES `transferencia` (`id_transferencia`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_cuentabancaria_empresa_moneda1`
    FOREIGN KEY (`moneda_id_moneda`)
    REFERENCES `moneda` (`id_moneda`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_cuentabancaria_empresa_tipo_cuenta_bancaria1`
    FOREIGN KEY (`tipo_cuenta_bancaria_id_tipo`)
    REFERENCES `tipo_cuenta_bancaria` (`id_tipo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_cuentabancaria_empresa_tgsi_e01_empresa1`
    FOREIGN KEY (`tgsi_e01_empresa_id_empresa`)
    REFERENCES `tgsi_e01_empresa` (`id_empresa`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `condicion_pago`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `condicion_pago` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `condicion_pago` (
  `id_condpago` INT NOT NULL,
  `fecha_pago` DATE NULL,
  `forma_pago` VARCHAR(45) NULL,
  `importe_pago` DECIMAL(19,2) NULL,
  `gcom_exx_ordencompra_id_orden` INT NOT NULL,
  PRIMARY KEY (`id_condpago`, `gcom_exx_ordencompra_id_orden`),
  INDEX `fk_condicion_pago_gcom_exx_ordencompra1_idx` (`gcom_exx_ordencompra_id_orden` ASC),
  CONSTRAINT `fk_condicion_pago_gcom_exx_ordencompra1`
    FOREIGN KEY (`gcom_exx_ordencompra_id_orden`)
    REFERENCES `gcom_exx_ordencompra` (`id_orden`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
