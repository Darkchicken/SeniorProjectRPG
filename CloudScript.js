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

handlers.setCustomDataToGrantedItem = function(args)
{
  var request = server.UpdateUserInventoryItemCustomData({
    PlayFabId: currentPlayerId,
    CharacterId: args.characterId,
    ItemInstanceId: "74D47A4D583418D7",
    Data: ["Active","0"]
  });
}

handlers.grantItemsToCharacter = function (args)
{
  var request = server.GrantItemsToCharacter({
    PlayFabId: currentPlayerId,
    CharacterId: args.characterId,
    ItemIds: args.items
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

