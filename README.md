Mass Transit Distributed Sagas with multiple consumers
======================================================

Getting it Running
------------------
1. Create a database to store the saga, subscription, health and timeout tables.
1. Modify if needed the connection string in Common.Constants.cs to point to the database created in step 1.
1. Make sure MSMQ is installed.
1. Set the following project to start:
	* Consumer.Worker1
	* Consumer.Worker2
	* MTSubscriptionRouter
	* Workflow.Worker1
1. Open the WCFTestClient - should be located here: "C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\WcfTestClient.exe"
	1. Right click "My Service Projects" and select "Add Service".
	1. Use the following address: "http://localhost:9999/Workflow1" (unless you changed it in Common.Constants.cs).
	1. Click "OK".
1. Double click the "StartWorkflow()" item in the treeview.
1. Edit the UserId to a number other then 0.
1. Click "Invoke".

Project Layout
--------------
**Common:** 
Contains constants useded accross the solution. Contains a util class used to write to conosle and setup commun bus settings
	
**Consumer.Common:** 
Contains the consumer used by Consumer.Worker1 and Consumer.Worker2

**Consumer.Worker1:** 
Exposes the consumer in Consumer.Common as a worker on the bus

**Consumer.Worker2**: 
Exposes the consumer in Consumer.Common as a worker on the bus

**Messages:** 
Contains all messages that can be sent over the bus.

**MTSubscriptionRouter:** 
Initializes the Subscription, Health and Timout sagas on the bus.

**Workflow.Common:**
Contains the Workflow Saga used by Workflow.Worker1 and Workflow.Worker2. Contains the WCF Service along with the required request and response objects used in the WCF methods.

**Workflow.Worker1:**
Exposes the saga in Workflow.Common as a worker on the bus.
	
**Workflow.Worker2:**
Exposes the saga in Workflow.Common as a worker on the bus.