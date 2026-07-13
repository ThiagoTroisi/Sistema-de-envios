CREATE DATABASE SistemaDeEnvios;
GO

USE SistemaDeEnvios;
GO

CREATE TABLE Perfil (
    id_perfil INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    activo BIT NOT NULL DEFAULT 1,
    dvh VARCHAR(255) NULL
);

CREATE TABLE Familia (
    id_familia INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    activo BIT NOT NULL DEFAULT 1,
    dvh VARCHAR(255) NULL
);

CREATE TABLE Permiso (
    id_permiso INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    dvh VARCHAR(255) NULL
);

CREATE TABLE Perfil_Familia (
    id_perfil INT,
    id_familia INT,
    dvh VARCHAR(255) NULL,

    PRIMARY KEY (id_perfil, id_familia),

    FOREIGN KEY (id_perfil) REFERENCES Perfil(id_perfil),
    FOREIGN KEY (id_familia) REFERENCES Familia(id_familia)
);

CREATE TABLE Perfil_Permiso (
    id_perfil INT,
    id_permiso INT,
    dvh VARCHAR(255) NULL,

    PRIMARY KEY (id_perfil, id_permiso),

    FOREIGN KEY (id_perfil) REFERENCES Perfil(id_perfil),
    FOREIGN KEY (id_permiso) REFERENCES Permiso(id_permiso)
);

CREATE TABLE Familia_Permiso (
    id_familia INT,
    id_permiso INT,
    dvh VARCHAR(255) NULL,

    PRIMARY KEY (id_familia, id_permiso),

    FOREIGN KEY (id_familia) REFERENCES Familia(id_familia),
    FOREIGN KEY (id_permiso) REFERENCES Permiso(id_permiso)
);

CREATE TABLE Familia_Familia (
    id_familia_padre INT,
    id_familia_hija INT,
    dvh VARCHAR(255) NULL,

    PRIMARY KEY (id_familia_padre, id_familia_hija),
    FOREIGN KEY (id_familia_padre) REFERENCES Familia(id_familia),
    FOREIGN KEY (id_familia_hija) REFERENCES Familia(id_familia),

    CHECK (id_familia_padre <> id_familia_hija)
);

CREATE TABLE Idioma (
    id_idioma INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50),
    dvh VARCHAR(255) NULL
);

CREATE TABLE Usuario (
    dni INT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    id_perfil INT NOT NULL,
    bloqueado BIT NOT NULL DEFAULT 0,
    estado BIT NOT NULL DEFAULT 1,
    intentos_fallidos INT NOT NULL DEFAULT 0,
    id_idioma INT NOT NULL DEFAULT 1,
    dvh VARCHAR(255) NULL,

    FOREIGN KEY (id_perfil) REFERENCES Perfil(id_perfil),
    FOREIGN KEY (id_idioma) REFERENCES Idioma(id_idioma)
);

CREATE TABLE Evento (
    id_evento INT IDENTITY(1,1) PRIMARY KEY,
    dni_usuario INT NOT NULL,
    fecha DATETIME NOT NULL DEFAULT GETDATE(),
    modulo VARCHAR(100) NOT NULL,
    evento VARCHAR(100) NOT NULL,
    criticidad INT,
    dvh VARCHAR(255) NULL,

    FOREIGN KEY (dni_usuario) REFERENCES Usuario(dni)
);

CREATE TABLE DVV
(
    tabla VARCHAR(100) PRIMARY KEY,
    dvv VARCHAR(255) NULL
);

INSERT INTO Idioma (nombre, dvh)
VALUES
('Español', 'dx8d9bOVRNwzpqYBCSojgXAEX23Il7bhpgN4kwEETis='),
('English', 'p11WWRjsIW623CneVGy5s/oUP38lvARbxMzTLzbS6j0='),
('Português', 'SSA/Hj1vinxhGZkeUnxluAJkk+Oj1xwVdzAlA7HF6Aw=');

INSERT INTO Perfil (nombre, dvh)
VALUES
('Administrador', 'hx10p2cqZeRwfI57C1MTu+rBOtXIGuzXChhbE8KANEQ='),
('Recepcionista', 'tYXOx6u0bLtp3zY+XEspvDl/11j+lg6iJfEH+tlainc='),
('Gestor', '3PiXTLcpL/j8+cT0hGnYoZjdvYg+/EeMnI1v+L4AduM='),
('Repartidor', '6fHC2zzOl3yy6+1UeNSymO8B8OmPjlh8qw3pj9tVjF8='),
('Remitente/Destinatario', 'g9E+8pShjFo2U6CI9qk45f4ZZyJYLmH0V4e0vmSmWf4=');

INSERT INTO Permiso (nombre, dvh)
VALUES
('Cambiar contraseña', 'H+E5DLBVNay4E6fW8dM0KkIvB+MVj+J/5UNTeZChMLE='),
('Cambiar idioma', 'teqn75buySzaZzNG4t3aRBbGjk0KRo8+biKNi1PJRO4='),
('Gestión de usuarios', 'hcEnHEEbo8nG6UE3SaEmnB4La8Gdl2XojHeGl3zkqI8='),
('Bitácora de eventos', 'tOq45Yiw61plHyoB2im738bOPLSS3lH/xMYyJQaEwTU='),
('Gestión de perfiles', 'EqMgaJJaX5y8OxHbHDpCIJomhn2GgSktb96SbLThEBY='),
('Gestión de backup', '6SfoesT4m9oReycKM7sRr0uVihB4vHRSknX0YSI3M2I='),
('Solicitar envío', 'RVUu4bGEr/OaN6/+qYEkSA9TZ9AceXggMkHhC8etKFg='),
('Registrar envío', 'K+GJXbUlZXjNSXHPQMuNi4AIfzZsaF8mPYpvMPXpcsg='),
('Consultar estado', 'aFZw+2jvZbErvU/LaWkrU+Dl/n5/SWnVzauqcyLKq+w='),
('Modificar destino', 'bmdh3hhTu2hul3BUg51CXdHlg4NtFoBw2bzrTnaeEgI='),
('Cancelar envío', 'xqHtX26GbQmSN7iiWtau5OQXRnxzZqzetbJ2rnIuaxE='),
('Registrar pago', 'OSi+JJgT1n3C2bUi0k3QoJiN7iZp69tsoDcgbGSM1NQ='),
('Consultar comprobantes', 'kkEDsk+rXoFV4XdPXJXiwRfNjVUbOkDPwkJf2uQS+m4='),
('Generar rutas', 'sWollaKRMiu3rURxBzJgI5KjocitOTX5MBP7bWXnor4='),
('Asignar rutas', '9b/j5ODpEXAlPASEd52AzZVlDwB1hD6Ts65Fxb2R9QE='),
('Consultar rutas', 'WgEZTLKJBWXN4tSo5kiNNJezrzAFAKsRMInLU8MS5fk='),
('Registrar entrega', 'hrfcJK2o6Wrz1ww0mAYIjjWWGaUujv2iIaU954WPi/Q='),
('Solicitar devolución', 'CbVGI5eyX1XcEonuOWQYnVPyXPNpbADY1OT8i4AsqVs='),
('Calificar repartidor', '++nxleK7r+wEThtxj3hkG0VeDmPPXRPCF9CGk3JJyk8='),
('Reportar inconveniente', 'OlxeXwLNdgeJlgYuNezw1Zp6zkVTUm6nuTFxacbFBpY='),
('Reporte de envíos', 'wGS7fDp5gG3aaPYS/6MGd5ufdlbsPxeRbHR4ZmsK6nA='),
('Reporte de entregas', 'Q3f3Q4DBjYdKPivA6GxQDSu1Om9gyYVkdV/jt479Zl0='),
('Reporte de inconvenientes', 'ZzxcK7xMduOHohLrSDse7XQWXUc4mNomv7+G4MOIgj8='),
('Reporte de repartidores', 'HJIBxQLNjB4Z3QZxmduhEbw/Y2PeHBHbnobsBhcimi8='),
('Reporte de pagos', 'fvY+YY5RqLEoz2BTsyj9jEdX8hEb0jumkDWyi4n6s1A='),
('Reporte de analíticas operativas', 'PrAdtPb1hnUj7TtlV2gguyG4U3jBJZWlBr1lGqGS5Mc=');

INSERT INTO DVV(tabla, dvv)
VALUES
('Perfil', 'H7XXi0TAbRXGs2zpMq3TMJiHaQXoveb+PG+71f6Nmuc='),
('Familia', ''),
('Permiso', 'RzVMTgLdQj424cEowNlti0LGuOVPTycOwL4NfudFqAY='),
('Perfil_Familia', ''),
('Perfil_Permiso', 'BQ1nFcHPeC1dI3fLU05XR7bQmXgZjz3eGJz5w/yxqfI='),
('Familia_Permiso', ''),
('Familia_Familia', ''),
('Idioma', '11RGkmmGmKxFdtPT72JnyBSE4qBNhdFZBrTmw8mfZIM='),
('Usuario', 'V2cmMqAlpNq0WEf0eoVpn0p90+6Pa/gUqNz9mq2NT7g='),
('Evento', '');

-- Administrador base
INSERT INTO Usuario (dni, nombre, apellido, email, password, id_perfil, dvh) VALUES (12345678, 'Administrador', 'Sistema', 'admin@sistema.com', '$2a$11$lVbVvtP4dYgekTqzbwg2zOpEOIoEKEkyuckdej7hbDo/vvOS69dMO', 1, 'ki7e9KBIii3mEKYZQUf+eoZKHC8n5Kd02xU7O7WHsVI=');
-- La contraseña en el programa es admin123

INSERT INTO Perfil_Permiso (id_perfil, id_permiso, dvh)
VALUES
(1, 1, 'T8grJq7LR9KGjE7741gXMqPny8xsLvsyBiwIFwoF7rg='),
(1, 2, 'a1HUMd9dfxQcvs7M957fPdhhw7QGnwsRZho+76y7qRg='),
(1, 3, 'P9ujXwTcjEYphsmSvPh1VGJXETByqQnBYvfkcOWB4ng='),
(1, 4, 'hSeokeIkE2lQ/zLKIStFvJP2n7uAHDsevtrFJ3X5nmE='),
(1, 5, '5in6ZZjXMnaPfHJrS2IShfnDuFMDkAqpEgF9t2F9i9s='),
(1, 6, 'sX720Zx6Wx7oO5B8WVUm3LHrBtuCJ9ZQ1d2gqfTOjNk='),
(1, 7, 'RSNUDxUEzRcQDEg16Ft+79SZEVgPjv/wWZqPKDvmueM='),
(1, 8, 'TslZn8ID0XajAVNsLgkaGbyFJ1myVb1oGIEKQsX+0Uo='),
(1, 9, 'lADxshy1J9f6PT6rupNVehjr56LKTkcc/l5MW0yn92c='),
(1, 10, 'm9sq9nmSBKKZxgOZS45ADksf1iXv23QGbMhp/uQsnfM='),
(1, 11, '9uCh4qxBlFqap/+KiqoM68EqO8yYGpKa1c+BCgkOEa4='),
(1, 12, 'sVVt6jLp0M2/7QOP13hydXdepAk5wUamTiBbyzSa0C8='),
(1, 13, 'bGWO6D+36BJIJJTz5BaodvY/QYoLih9edtR+5BdwNcs='),
(1, 14, 'nx+dzjGcRwDvKOyMU708yOar5kxoOFR5q4khWAalvdY='),
(1, 15, 'KNrnyL3i88pgj4bQ4WohTe50x0vuARzf3Ua8BLZVvBQ='),
(1, 16, '5bhhptipZt/KfnNBzT62vpkBaI1UenLr7QsfXhTz0I0='),
(1, 17, 'Ksh4sOIYBhaZO0tqpx5hFm/chsKNR+NZ0O5TfrEdRtM='),
(1, 18, 'hdqvb3BVzVc2KH+u2WA9cSkgCSxPj9AJfsO2UL8nUw4='),
(1, 19, 'MDi/tXW+5qDmGUXv+HhINbsscgY05Cc0Z4wIOZS38Bg='),
(1, 20, 'KrrKSRHmj6m/vzSC7nl/1bkEW4Qf3/clNVfF/hXeZHc='),
(1, 21, 'iaoeWAAjci22dkboFJ6yRsdI4YDjShz2easLQaQW2QQ='),
(1, 22, 'G+ADQQguJcTiUcpnE+dn9xMaKCOwBSyvnJsAbsUS9ss='),
(1, 23, 'pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM='),
(1, 24, 'av/a47PBqmqnaJ6bansyJaY2qhrAAl9JDMoShc6vFIc='),
(1, 25, 'D47zN3sw/Ef5a0gkf0Y6cmqAL2Lz+qA9VkA3UdL2bGc='),
(1, 26, 'ZaaZkFwCYZNwvPkgf1pHfD1nEwynHsb3UOB/6NUQsIQ=');