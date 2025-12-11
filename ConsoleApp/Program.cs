//instrukcje najwyższego poziomu(top - level statements) - są to instrukcje, które są wykonywane bezpośrednio w pliku, bez potrzeby definiowania klasy lub metody głównej.
//wszystko co jest napisane w tym pliku jest traktowane jako kod, który będzie otoczony klasą Program i metodą Main podczas kompilacji


using ConsoleApp;
using ConsoleApp.Delegates;

//Introduction.Run();
//Classes.Run();

DelegatesExample delegatesExample = new DelegatesExample();
delegatesExample.Test();

MulticastDelegateExample multicastDelegateExample = new MulticastDelegateExample();
multicastDelegateExample.Test();

BuildInDelegatesExample buildInDelegatesExample = new BuildInDelegatesExample();
buildInDelegatesExample.Test();