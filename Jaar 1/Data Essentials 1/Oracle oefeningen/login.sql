-- LOGIN.SQL
-- SQL*Plus instellingen wijzigen

-- Breedte vensterafdruk
SET LINESIZE 200

-- Hoogte vensterafdruk
SET PAGESIZE 100

-- Datumformaat wijzigen
alter session
set  	nls_date_format = "dd-mm-yyyy";
nls_language=Dutch
nls_currency='EUR'
nls_numeric_characters=' ';

Prompt === Datum gewijzigd. ===


