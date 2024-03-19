<b> Ce projet a pour but de mettre en action un exemple complet d'une application réalisée en blazor

L'architecture de projet : clean architecture

Le projet est composé de 3 solutions qui se complètent :
-
- Blazor server : Front office 
- WebAssembly : Back office
- WebAPI : API Restfull

Freamwork utiliser .net6/.net cor.
Pour démarrer le projet, il faut remplacer la chaîne de connexion '<b>DefaultConnection</b>' par une chaîne valide sur votre base donnée SqlServeur.
Pour générer la base donnée, il faut juste lancer le projet API et la base va se créer automatiquement via les migration Code first.

Fonctionalite blazor principale :
- 
- <b> Parameter
- <b> CascadingParameter
- <b> EventCallback
- <b> AuthenticationState
- <b> Component
- <b> Pages
- <b> HttpClient
- <b> Life cycle (OnInitializedAsync,OnParametersSet,OnAfterRender)
