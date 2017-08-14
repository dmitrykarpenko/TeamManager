"use strict";

var PlayersPageVM = function (vmData) {
    var self = this;

    var po = Object.keys(context.position).map(function (x, i) {
        return {
            Id: context.position[x],
            Name: x
        };
    });
    self.positonOptions = ko.observableArray(po);
    self.availableTeams = toArrayOfTeamVMs(vmData.AvailableTeams);

    self.players = ko.observableArray(toArrayOfPlayerVMs(vmData.Players, self.availableTeams));
    self.players.errors = ko.validation.group(self.players);//, { deep: true, live: true });

    self.pageInf = vmData.PageInf;

    self.newPlayer = ko.observable(createDefaultPlayerVM());
    self.newPlayer.errors = ko.validation.group(self.newPlayer);

    self.countOfAllPlayers = ko.observable(vmData.CountOfAllPlayers);

    self.message = ko.observable("");

    function createDefaultPlayerVM() {
        return new PlayerVM(null, null, null, self.availableTeams);
    };

    self.addNewPlayer = function () {
        self.players.push(createDefaultPlayerVM());

        ++self.pageInf.PageSize;
        koHelpers.increment(self.countOfAllPlayers);

        $(".selectpicker").selectpicker("render");
    };

    self.deletePlayer = function (player) {
        if (player.Id !== context.emptyGuid)
            $.ajax({
                url: "/Player/Delete",
                type: "POST",
                data: ko.toJSON({ id: player.Id }),
                contentType: "application/json",
                success: function () {
                    self.players.remove(function (s) { return s.Id === player.Id; });
                    self.message(player.Name() + " removed");

                    --self.pageInf.PageSize;
                    koHelpers.decrement(self.countOfAllPlayers);
                }
            });
        else {
            self.players.remove(function (s) { return s.Id === player.Id; });

            --self.pageInf.PageSize;
            koHelpers.decrement(self.countOfAllPlayers);
        }
            
    };

    self.saveAll = function () {
        self.players.errors.showAllMessages();
        var allAreValid = self.players.errors().length == 0;

        if (allAreValid)
            $.ajax({
                url: "/Player/Save",
                type: "POST",
                data: ko.toJSON(self.players),
                contentType: "application/json",
                success: function (data) {
                    ////rebinds every player as observable, right now not required
                    //ko.mapping.fromJS(data.players(), {}, self.players);
                    self.players(toArrayOfPlayerVMs(data.players, self.availableTeams));
                    self.message("All players saved in DB");

                    $(".selectpicker").selectpicker("render");
                }
            });
    };

    self.getPage = function () {
        $.ajax({
            url: "/Player/GetPage",
            type: "POST",
            data: ko.toJSON(self.pageInf),
            contentType: "application/json",
            success: function (data) {
                self.players(toArrayOfPlayerVMs(data.Players, self.availableTeams));
                self.availableTeams = toArrayOfTeamVMs(data.AvailableTeams);
                self.countOfAllPlayers(data.CountOfAllPlayers);
                self.message("Players retrieved successfully");

                $(".selectpicker").selectpicker("render");
            }
        });
    };

    self.increasePageSizeAndGetPage = function (increaseBy) {
        self.pageInf.PageSize += increaseBy;
        self.getPage();
    };
    self.setPageInfAndGetPage = function (newPageInf) {
        self.pageInf = newPageInf;
        self.getPage();
    };

    //self.exportToFile = function () {
    //    //write formatted data to table.json
    //    var blob = new Blob([ko.toJSON(self, null, 2)], { type: "text/json;charset=utf-8" });
    //    saveAs(blob, "table.json");
    //}

    self.saveNewPlayer = function (vmData) {
        self.newPlayer.errors.showAllMessages();
        var isValid = self.newPlayer.errors().length == 0;
        if (isValid)
            $.ajax({
                url: "/Player/Save",
                type: "POST",
                data: ko.toJSON([vmData.newPlayer]),
                contentType: "application/json",
                success: function (data) {
                    ko.utils.arrayPushAll(self.players, toArrayOfPlayerVMs(data.players, self.availableTeams));
                    self.message(data.players[0].Name + " saved successfully");
                    self.newPlayer(createDefaultPlayerVM());

                    ++self.pageInf.PageSize;
                    koHelpers.increment(self.countOfAllPlayers);

                    $(".selectpicker").selectpicker("render");
                }
            });
    };
};

