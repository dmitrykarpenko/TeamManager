"use strict";

var TeamsPageVM = function (vmData) {
    var self = this;

    self.teams = ko.observableArray(toArrayOfTeamVMs(vmData.Teams));
    self.teams.errors = ko.validation.group(self.teams);//, { deep: true, live: true });

    self.pageInf = vmData.PageInf;
  
    self.newTeamPopup = createDefaultNewTeamPopupVM();
    function createDefaultNewTeamPopupVM() {
        return new NewTeamPopupVM(createDefaultTeamVM(), onSuccessfulNewTeamSaving, self)
    };
    function onSuccessfulNewTeamSaving(data, selfVMPar) {
        ko.utils.arrayPushAll(selfVMPar.teams, toArrayOfTeamVMs(data.teams));
        selfVMPar.message(data.teams[0].Name + " saved successfully");
        //newTeamPopup.newTeam is observable now
        selfVMPar.newTeamPopup.newTeam(createDefaultTeamVM());

        ++selfVMPar.pageInf.PageSize;

        selfVMPar.newTeamPopup.toggle();
    };
    function createDefaultTeamVM() {
        return new TeamVM();
    };

    self.message = ko.observable("");

    self.addNewTeam = function () {
        self.teams.push(createDefaultTeamVM());

        ++self.pageInf.PageSize;

        $(".selectpicker").selectpicker("render");
    };

    self.deleteTeam = function (team) {
        if (context.notEmptyId(team.Id))
            $.ajax({
                url: "/Team/Delete",
                type: "POST",
                data: ko.toJSON({ id: team.Id }),
                contentType: "application/json",
                success: function () {
                    self.teams.remove(function (g) { return g.Id === team.Id; });
                    self.message(team.Name() + " removed");

                    --self.pageInf.PageSize;
                }
            });
        else
            self.teams.remove(function (s) { return s.Id === team.Id; });
    };

    self.saveAll = function () {
        self.teams.errors.showAllMessages();
        var allAreValid = self.teams.errors().length == 0;

        if (allAreValid)
            $.ajax({
                url: "/Team/Save",
                type: "POST",
                data: ko.toJSON(self.teams),
                contentType: "application/json",
                success: function (data) {
                    ////rebinds every team as observable, right now not required
                    //ko.mapping.fromJS(data.teams(), {}, self.teams);
                    self.teams(toArrayOfTeamVMs(data.teams));
                    self.message("All teams saved in DB");

                    $(".selectpicker").selectpicker("render");
                }
            });
    };

    self.getPage = function () {
        $.ajax({
            url: "/Team/GetPage",
            type: "POST",
            data: ko.toJSON(self.pageInf),
            contentType: "application/json",
            success: function (data) {
                self.teams(toArrayOfTeamVMs(data.Teams));
                self.message("Teams retrieved successfully");

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
};

var NewTeamPopupVM = function (newTeamInitVal, onSuccessfulSaving, parentVM) {
    var self = this;

    self.newTeam = ko.observable(newTeamInitVal || new TeamVM());
    self.visible = ko.observable(false);

    self.toggle = function () {
        var vis = self.visible;
        vis(vis() ? false : true);
    }

    self.save = function () {
        self.newTeam().save(onSuccessfulSaving, parentVM);
    }
};