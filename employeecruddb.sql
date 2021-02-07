use EmployeeCRUDDB

-- DROP TABLE statement is used for testing purposes and error checking.
DROP TABLE EmployeeAccount

create table EmployeeAccount (
	employeeID INT IDENTITY PRIMARY KEY,
	employeeFirstName varchar(250) NOT NULL,
	employeeLastName varchar(250) NOT NULL,
	employeePosition varchar(250) NOT NULL,
	employeeOffice varchar(250) NOT NULL,
	employeeSalary int NOT NULL
)

-- data was inserted using the wizard, the full script will reveal this
-- this simply a create table/procedure query.