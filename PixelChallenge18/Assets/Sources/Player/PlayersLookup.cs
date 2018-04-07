using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayersLookup : MonoBehaviour {

    private Dictionary<PlayerData, Player> _lookup = new Dictionary<PlayerData, Player>();

    public void RegisterPlayer(Player player)
    {
        _lookup.Add(player.Data, player);
    }

    public void Cleanup()
    {
        _lookup.Clear();
    }

    public Player[] GetPlayers()
    {
        return _lookup.Values.ToArray();
    }

    public Player GetPlayer(PlayerData playerData)
    {
        if (_lookup.ContainsKey(playerData))
        {
            return _lookup[playerData];
        }
        return null;
    }

}
