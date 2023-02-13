---------------------------
-- Create schema objects --
---------------------------

PROMPT
PROMPT Specify password for app_data as parameter 1:
DEFINE pass_app_data = &1

CONNECT app_data/&pass_app_data

CREATE OR REPLACE TRIGGER employees#_aiufer
AFTER INSERT OR UPDATE OF salary, job_id ON employees# FOR EACH ROW
DECLARE
 l_cnt NUMBER;
BEGIN
LOCK TABLE jobs IN SHARE MODE; -- Ensure that jobs does not change
                               -- during the following query.
     SELECT COUNT(*) INTO l_cnt
     FROM jobs
     WHERE job_id = :NEW.job_id
     AND :NEW.salary BETWEEN min_salary AND max_salary;

     IF (l_cnt<>1) THEN
       RAISE_APPLICATION_ERROR( -20002,
         CASE
           WHEN :new.job_id = :old.job_id
           THEN 'Salary modification invalid'
           ELSE 'Job reassignment puts salary out of range'
         END );
     END IF;
END;
/
