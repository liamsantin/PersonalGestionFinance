INSERT INTO TA_ADDRESS (addr_street, addr_postalCode, addr_city, country_id)
VALUES 
    ('Rue du Lac 12', '1200', 'Genève', 2)
	
-- INSERT INTO TA_USER ('Santin', 'Liam', 'ls', '')


DROP TABLE TA_ADDRESS;
CREATE TABLE TA_ADDRESS (
    addr_id        INTEGER PRIMARY KEY AUTOINCREMENT,
    addr_street    TEXT NOT NULL,
    addr_postalCode TEXT NOT NULL,
    addr_city      TEXT NOT NULL,
    country_id   INTEGER,
	FOREIGN KEY (country_id) REFERENCES TA_COUNTRY(country_id) ON DELETE SET NULL
);