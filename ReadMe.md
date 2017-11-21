# Examples

This is an example app and it's purpouse is to demonstrate usage of some libraries  
  
[M.Executables](https://github.com/petar-m/executables)  
[M.Executables.Executors.SimpleInjector](https://github.com/petar-m/executables.executors)  
[M.Repository](https://github.com/petar-m/repository)  
[M.Repository.EntityFramework](https://github.com/petar-m/repository.ef)  
[M.ScheduledAction](https://github.com/petar-m/scheduledaction)  
[M.EventBroker](https://github.com/petar-m/eventbroker)  

What it does:  
  
 - generates a "word" every 10 seconds  
 - generates a "sentence" from the last 10 generated "words"  
 - stores the "sentence" 
 - provides a HTTP API that allows  
 	- querying "sentence"s
 	- generating "sentences" by providing "words"  
 	
***NOTE: This is not intended to demonstrate how an API should be built or code organised, it's an example of how some libraries may be used.*** 
  
In order to run the app a URL reservation is needed (execute with elevated priviliges)  

    netsh http add urlacl url=http://+:8989/ user="Everyone"

