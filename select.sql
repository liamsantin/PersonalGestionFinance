SELECT 
    a.addr_id,
    a.addr_street,
    a.addr_postalCode,
    a.addr_city,
    c.country_name,
    c.country_iso
FROM TA_ADDRESS a
INNER JOIN TA_COUNTRY c 
    ON a.country_id = c.country_id;