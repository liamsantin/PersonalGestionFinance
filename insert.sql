INSERT INTO TA_ADDRESS (addr_street, addr_postalCode, addr_city, addr_country)
VALUES 
    ('Rue du Lac 12', '1200', 'Genève', 'CH'),   -- id = 1
    ('Avenue Centrale 45', '1003', 'Lausanne', 'CH'), -- id = 2
    ('Via Roma 8', '6900', 'Lugano', 'CH');      -- id = 3


INSERT INTO TA_USER (user_nom, user_prenom, user_email, user_password, user_iban, user_phone, addr_id)
VALUES
    ('Santin', 'Liam', 'lsantin312005@gmail.com', 'password', 'CH9300762011623852957', '+41791234567', 1);
