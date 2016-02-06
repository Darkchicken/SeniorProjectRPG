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

handlers.grantItemsToCharacter = function (args)
{
  var request = server.GrantItemsToCharacter({
    PlayFabId: currentPlayerId,
    CharacterId: args.characterId,
    CatalogVersion: args.catalogVersion,
    ItemIds: ["Runes_Slam"]
  });
  return request;		
}

handlers.revokeInventoryItem = function (args)
{
  var request = server.RevokeInventoryItem({
    PlayFabId: currentPlayerId,
    CharacterId: args.characterId,
    ItemInstanceId: args.itemId
  });
  return request;		
}


handlers.grantItemsToUser = function (args)
{
  var request = server.GrantItemsToUser({
    PlayFabId: currentPlayerId,
    ItemIds: [args.itemId]
  });
  return request;		
}

handlers.moveItemFromUserToCharacter = function (args)
{
  var request = server.GrantItemsToCharacter({
    PlayFabId: currentPlayerId,
    CharacterId: args.characterId,
    ItemInstanceId: args.itemInstanceId
  });
  return request;		
}

