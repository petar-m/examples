# Examples

This is an example app and it's purpouse is to demonstrate usage of some libraries  
  
[M.Executables](https://github.com/petar-m/executables)  
[M.Executables.Executors.SimpleInjector](https://github.com/petar-m/executables.executors)  
[M.Repository](https://github.com/petar-m/repository)  
[M.Repository.EntityFramework](https://github.com/petar-m/repository.ef)  
[M.ScheduledAction](https://github.com/petar-m/scheduledaction)  
[M.EventBroker](https://github.com/petar-m/eventbroker)  

Reqirements:  
  
 - generate a "word" every 10 seconds  
 - generate a "sentence" from the last 10 generated "words"  
 - store the "sentence" 
 - provide a HTTP API that allows  
 	- queriyng "sentence"s
 	- generating "sentences" by providing "words"
 
URL reservation  
netsh http add urlacl url=http://+:8989/ user="Everyone"

