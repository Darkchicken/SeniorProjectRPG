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

handlers.grantItemToCharacter = function (args)
{
  var request = server.GrantItemsToCharacter({
    PlayFabId: args.playFabId,
    CharacterId: args.characterId,
    ItemIds: [args.itemId]
  });
  return request;		
}

handlers.grantItemToUser = function (args)
{
  var request = server.GrantItemsToUser({
    PlayFabId: args.playFabId,
    ItemIds: [args.itemId]
  });
  return request;		
}

handlers.moveItemFromUserToCharacter = function (args)
{
  var request = server.GrantItemsToCharacter({
    PlayFabId: args.playFabId,
    CharacterId: args.characterId,
    ItemInstanceId: args.itemInstanceId
  });
  return request;		
}

