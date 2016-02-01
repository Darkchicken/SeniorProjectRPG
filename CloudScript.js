handlers.helloWorld = function (args)
{
  var message = "Hello " + currentPlayerId + "!";
  log.info(message);
  return { messageValue: message };
}

handlers.newCharacter = function (args)
{
  var characterID = server.GrantCharacterToUser({
    PlayFabId: currentPlayerId,
    CharacterName: args.characterName,
    CharacterType: args.characterType
  });
  return characterID;		
}