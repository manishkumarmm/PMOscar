/****** Script for Adding new column CostCentreId to the Resource Table  ******/

  
	ALTER TABLE Resource
	ADD CostCentreID int null
	
	
	/****** Script for adding new column CostCentreID to Project table  ******/

  
	ALTER TABLE  project add CostCentreID int null