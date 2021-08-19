CREATE database mqttdb
use mqttdb

CREATE TABLE device (
    id uniqueidentifier  NOT NULL PRIMARY KEY DEFAULT newid(),
    name varchar(64) NOT NULL,
    creating_date datetime2 NOT NULL,
    editing_date datetime2 DEFAULT NULL,
    geo text,
    descr text
);


CREATE TABLE measurements(
    id uniqueidentifier NOT NULL PRIMARY KEY DEFAULT newid(),
    id_device uniqueidentifier NOT NULL,
    date datetime2 NOT NULL,
    atmospheric_pressure float NOT NULL,
    temperature float NOT NULL,
    air_humidity float NOT NULL,
    light_level float NOT NULL,
    smoke_level float NOT NULL,
    FOREIGN KEY (id_device) REFERENCES device (id) ON DELETE CASCADE
);

CREATE TABLE events(
    id uniqueidentifier NOT NULL PRIMARY KEY DEFAULT newid(),
    id_device uniqueidentifier NOT NULL,
    date datetime2 NOT NULL,
    message text NOT NULL,
    FOREIGN KEY (id_device) REFERENCES device (id) ON DELETE CASCADE
);


