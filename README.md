Description:

	A WPF project to realize a user interface for the DiReCT program based on the mock-up of Johnson Su.
	
	Using C# .NET and WPF, the current version is under MVVM. While an older, more MVC version is also available as zip file. 

	A code sample from MADN is used in the project, related license can be found in the folder.
	
For beginners:

	To understand MVVM design pattern and RelayCommand, the TextBlockView and its view model is a simple and clear example to begin with.
	
	WPF map control is needed to run this program. To install WPF map control, run this line in TOOLS/Library Package Manager/Package Manager Console:
		
		Install-Package Microsoft.Maps.MapControl.WPF
	
Future works in order of importance:
	
	Future to-dos:
	
		Implementation of ObservationRecord and other models.
		Internet transmission and its security.
		Real-time validation module.
	
	Possible major improvements:
	
		Multi-threading on time-consuming works such as XML parsing and record validation.
		Implementation of new maps using GMAP.	
		Simplify the logic by using Stateless or other state machine models.
	
	Possible works in the future:
	
		Rework of the hard-coded parts.
		Work on 'background', 'task list' and 'recorder' according to future version of the mock-up.
	