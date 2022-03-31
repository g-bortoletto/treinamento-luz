CREATE DATABASE escola;

USE escola;

CREATE TABLE alunos (
	id VARCHAR(255),
	nome VARCHAR(255),
	sobrenome VARCHAR(255),
	data_nascimento DATETIME,
	matricula INTEGER
);

CREATE TABLE faxineiros (
	id VARCHAR(255),
	nome VARCHAR(255),
	sobrenome VARCHAR(255),
	data_nascimento DATETIME,
	salario REAL
);

CREATE TABLE professores(
	id VARCHAR(255) PRIMARY KEY,
	nome VARCHAR(255),
	sobrenome VARCHAR(255),
	data_nascimento DATETIME,
	salario REAL,
	disciplina VARCHAR(255)
);