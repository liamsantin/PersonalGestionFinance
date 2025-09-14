
DROP TABLE TA_USER;
CREATE TABLE TA_USER (
    user_id        INTEGER PRIMARY KEY AUTOINCREMENT,
    user_nom       TEXT NOT NULL,
    user_prenom    TEXT NOT NULL,
    user_email     TEXT NOT NULL UNIQUE,
    user_password  TEXT NOT NULL,
    user_iban      TEXT,
    user_phone     TEXT,
    addr_id        INTEGER,  -- référence à l'adresse principale
    user_createAt  DATETIME NOT NULL DEFAULT (datetime('now')),
    user_updateAt  DATETIME NOT NULL DEFAULT (datetime('now')),
    FOREIGN KEY (addr_id) REFERENCES TA_ADDRESS(addr_id) ON DELETE SET NULL
);

DROP TABLE TA_COUNTRY;
CREATE TABLE TA_COUNTRY (
	country_id		INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	country_name	CHAR(100) NOT NULL UNIQUE,
	country_iso		CHAR(2) NOT NULL UNIQUE
);

DROP TABLE TA_ADDRESS;
CREATE TABLE TA_ADDRESS (
    addr_id        INTEGER PRIMARY KEY AUTOINCREMENT,
    addr_street    TEXT NOT NULL,
    addr_postalCode TEXT NOT NULL,
    addr_city      TEXT NOT NULL,
    country_id   INTEGER,
	FOREIGN KEY (country_id) REFERENCES TA_COUNTRY(country_id) ON DELETE SET NULL
);
/*CHECK (
                      length(user_iban) = 21
                      AND substr(user_iban, 1, 2) = 'CH'
                    ),*/