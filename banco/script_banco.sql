CREATE DATABASE IF NOT EXISTS emprestimos
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE emprestimos;

CREATE TABLE IF NOT EXISTS itens (
    id                   INT          NOT NULL AUTO_INCREMENT,
    item                 VARCHAR(100) NOT NULL,
    nome                 VARCHAR(100) NOT NULL,
    contato              VARCHAR(100) NOT NULL,
    emprestimo           DATE         NOT NULL,
    `devolucao prevista` DATE         NULL,
    `devolucao real`     DATE         NULL,
    devolvido            TINYINT(1)   NOT NULL DEFAULT 0,
    PRIMARY KEY (id)
);
