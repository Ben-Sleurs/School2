----------------------
-- Create admin_pkg --
----------------------

PROMPT
PROMPT Specify password for app_admin as parameter 1:
DEFINE pass_app_admin = &1

CONNECT app_admin/&pass_app_admin

CREATE OR REPLACE PACKAGE admin_pkg
AS
  PROCEDURE update_job
    ( p_job_id IN jobs.job_id%TYPE,
      p_job_title IN jobs.job_title%TYPE := NULL,
      p_min_salary IN jobs.min_salary%TYPE := NULL,
      p_max_salary IN jobs.max_salary%TYPE := NULL );
  PROCEDURE add_job
    ( p_job_id IN jobs.job_id%TYPE,
      p_job_title IN jobs.job_title%TYPE,
      p_min_salary IN jobs.min_salary%TYPE,
      p_max_salary IN jobs.max_salary%TYPE );
  PROCEDURE update_department
    ( p_department_id IN departments.department_id%TYPE,
      p_department_name IN departments.department_name%TYPE := NULL,
      p_manager_id IN departments.manager_id%TYPE := NULL,
      p_update_manager_id IN BOOLEAN := FALSE );
  FUNCTION add_department
    ( p_department_name IN departments.department_name%TYPE,
      p_manager_id IN departments.manager_id%TYPE )
    RETURN departments.department_id%TYPE;
END admin_pkg;
/

CREATE OR REPLACE PACKAGE BODY admin_pkg
AS
  PROCEDURE update_job
    ( p_job_id IN jobs.job_id%TYPE,
      p_job_title IN jobs.job_title%TYPE := NULL,
      p_min_salary IN jobs.min_salary%TYPE := NULL,
      p_max_salary IN jobs.max_salary%TYPE := NULL )
  IS
  BEGIN
    UPDATE jobs
    SET job_title = NVL( p_job_title, job_title ),
    min_salary = NVL( p_min_salary, min_salary ),
    max_salary = NVL( p_max_salary, max_salary )
    WHERE job_id = p_job_id;
  END update_job;

  PROCEDURE add_job
    ( p_job_id IN jobs.job_id%TYPE,
      p_job_title IN jobs.job_title%TYPE,
      p_min_salary IN jobs.min_salary%TYPE,
      p_max_salary IN jobs.max_salary%TYPE )
  IS
  BEGIN
  INSERT INTO jobs ( job_id, job_title, min_salary, max_salary )
  VALUES ( p_job_id, p_job_title, p_min_salary, p_max_salary );
  END add_job;

  PROCEDURE update_department
    ( p_department_id IN departments.department_id%TYPE,
      p_department_name IN departments.department_name%TYPE := NULL,
      p_manager_id IN departments.manager_id%TYPE := NULL,
      p_update_manager_id IN BOOLEAN := FALSE )
  IS
  BEGIN
    IF ( p_update_manager_id ) THEN
     UPDATE departments
     SET department_name = NVL( p_department_name, department_name ),
     manager_id = p_manager_id
     WHERE department_id = p_department_id;
    ELSE
     UPDATE departments
     SET department_name = NVL( p_department_name, department_name )
     WHERE department_id = p_department_id;
    END IF;
  END update_department;

  FUNCTION add_department
    ( p_department_name IN departments.department_name%TYPE,
      p_manager_id IN departments.manager_id%TYPE )
      RETURN departments.department_id%TYPE
  IS
   l_department_id departments.department_id%TYPE := departments_sequence.NEXTVAL;
  BEGIN
   INSERT INTO departments ( department_id, department_name, manager_id )
   VALUES ( l_department_id, p_department_name, p_manager_id );
   RETURN l_department_id;
  END add_department;
END admin_pkg;
/

-------------------------------------------
-- Grant privileges on admin_pkg to user --
-------------------------------------------

GRANT EXECUTE ON admin_pkg TO app_admin_user;



