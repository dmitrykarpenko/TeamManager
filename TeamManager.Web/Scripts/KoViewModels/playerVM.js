"use strict";

var PlayerVM = function (id, name, teams, availableTeams) {
    var self = this;

    self.Id = id || null;
    self.Name = ko.observable(name || "").extend({ required: true });//"Please enter player's a name" });
    self.Teams = ko.observableArray(teams || []);

    self.SelectedTeamId = ko.observable(null);
    self.SelectedTeamId.subscribe(function (val) {
        if (val) {
            var newTeam = availableTeams.find(function (x) {
                return x.Id == val;
            });

            if (newTeam && !self.Teams().some(function (x) { return x.Id == newTeam.Id; })) {
                self.Teams.push(newTeam);
            }
            
            self.SelectedTeamId(null);
        }
    });
};

var toArrayOfPlayerVMs = function (players, availableTeams) {
    var playerVMs = ko.utils.arrayMap(players, function (player) {
        return new PlayerVM(player.Id, player.Name, player.Teams, availableTeams);
    });
    return playerVMs;
};