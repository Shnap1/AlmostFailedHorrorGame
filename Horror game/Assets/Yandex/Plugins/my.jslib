mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, world!");
    console.log("Hello, world!");

  },
  GiveMePlayerData: function () {
    console.log(player.getName);
    console.log(player.getPhoto("medium"));

  }
});