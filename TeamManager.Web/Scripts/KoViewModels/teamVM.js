"use strict";

var TeamVM = function (id, name) {
    var self = this;

    self.Id = id || 0;
    self.Name = ko.isComputed(name) ? name : ko.observable(name || "");
    self.Name.extend({ required: true });//"Please enter team's name" });
    self.errors = ko.validation.group(self, { deep: false });

    self.save = function (onSuccess, parentVM) {
        self.errors.showAllMessages();
        var isValid = self.errors().length == 0;
        if (isValid)
            $.ajax({
                url: "/Team/Save",
                type: "POST",
                data: ko.toJSON([self]),
                contentType: "application/json",
                success: function (data) {
                    if (onSuccess && parentVM)
                        onSuccess(data, parentVM);
                }
            });
    };
};

var toArrayOfTeamVMs = function (teams, getTeamById) {
    var teamVMs = ko.utils.arrayMap(teams, function (team) {
        return new TeamVM(team.Id, team.Name);
    });
    return teamVMs;
};