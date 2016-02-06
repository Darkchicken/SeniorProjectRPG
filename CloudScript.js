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

handlers.grantItem = function (args)
{
  var request = server.GrantItemsToCharacter({
    PlayFabId: currentPlayerId,
    CharacterId: args.characterId,
    ItemIds: [args.itemId]
  });
  return request;		
}