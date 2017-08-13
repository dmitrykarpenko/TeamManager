"use strict";

var PlayerVM = function (id, name, teamId, availableTeams) {
    var self = this;

    self.Id = id || 0;
    self.Name = ko.observable(name || "").extend({ required: true });//"Please enter player's a name" });
    self.TeamId = ko.observable(teamId || null);

    self.Team = ko.computed(function () {
        if (!self.TeamId() || !availableTeams || availableTeams.length == 0)
            return null;
        var avTeamsById = $.grep(availableTeams, function (ag) {
            return ag.Id == self.TeamId();
        });
        return avTeamsById[0] || null;
    }, self);
    //self.Team = ko.observable(team ? new TeamVM(team.Id, team.Name) : new TeamVM());
};

var toArrayOfPlayerVMs = function (players, availableTeams) {
    var playerVMs = ko.utils.arrayMap(players, function (player) {
        return new PlayerVM(player.Id, player.Name, player.Team ? player.Team.Id : null, availableTeams);
    });
    return playerVMs;
};