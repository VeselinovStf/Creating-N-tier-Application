# Creating-N-tier-Application
Pluralsight Creating N-Tier Applications in C#  Course Repo

## [Part 1](https://github.com/VeselinovStf/Creating-N-tier-Application/tree/master/Part_01 "Part 1") 

In part 1 of the course, goal is to  learn the pros and cons of separating applications into tiers, and two different ways to go about doing so.

"In part 1 of this series, you'll learn why separating software application logic into layers is a common practice, as well as some pros and cons of doing so. You'll see how monolithic applications can evolve into N-Tier applications, including two approaches: Data Centric and Domain Centric (or DDD) designs."

"In part 2 of this series, you'll continue to learn about building N-Tier applications using domain driven design techniques. You'll learn how to structure tests within your solution, how to design your application's persistence strategy, and how to reuse your core domain objects between multiple front-end applications."

NOTE: Part 1 and Part 2 are all in Part 1 folder of repo

### Repo Structure ( different parts of cours are separated in different branch )

#### Branches

	- 	master 
	-	Api_CLI_Update_WebClient CLI-Client
	-	API
	- 	Part2_Core_Testing
	-	Domain Centric Design
	-	Api_CLI_Update_WebClient
	-	CLI-Client
	
### Project

-  Sample Application – A Social Web Site 

## Summary

-  N-Tier Design can refer to both the logical and physical separation of responsibilities within an application  
- The terms tier and layer may be used interchangeably, though each has specific meaning as well  
- Logical separation into layers can improve code maintainability  
- Physical separation into tiers can provide scalability, security, and fault tolerance, among other benefits  
- Applications can evolve over time as their needs require the complexity of an N-Tier design  
- Avoid making end runs around layers in your application 

## Traditional Data-Centric N-Tier Architecture 

-  Build the Add Friend feature into PluralsightBook 
	-  New Feature:   Add Friend , Remove Friend 
-  Start building everything in the UI 
-  Refactor common logic into BLL (Business Logic Layer ) and DAL ( Data access Layer )

### TODO LIST

X - Add Menu Link To friends
X - Create Friends Page
X - Show Current Friends on Page
X - Link to Add Friend page
X - Create Add Frient Page
X - Remove on current Friends Page

### More Requirements

	-	Transform all to Domain Centric Design
	-	Add Tests
	-	Add API
	-	Add CLI Client that calls API for friend list ( not fully implemented! )
