var LantaarnGroup = new THREE.Group();

function StraatLantaarns(x, y, z) {

    LoadOBJModel("obj/StraatLantaarns/", "lamp.obj", "obj/StraatLantaarns/", "lamp.mtl", (mesh) => {
        mesh.position.z = 42;
        mesh.position.set(x, y, z);
        mesh.scale.set(0.5, 0.5, 0.5);
        LantaarnGroup.add(mesh);

    })


    return LantaarnGroup;



}

function StraatLighting(x,y,z) {
    
    var light = new THREE.SpotLight(0xFFFFFF, 4);
    //light.position.set(-40, 8, 55);
    light.position.set(x, y, z);
   // light.target = t;
    light.distance = 40;
    light.angle = 0.2;
  

    return light;
}

function StraatLantaarnSpawn()
{
    var naam = [];
    var lichtvar = [];
    var Lantaarns = [];
    var SpawnGroup = new THREE.Group();

    for (var i = 0; i < 6; i++) {
        //vullen met naam variablen
        naam.push("Lantaarn" + i.toString());
        //lantaarn0 is nieuwe lantaarn
        //lantaarn0 = straatlantaarn..
        lichtvar.push("lichtje" + i.toString());

    };

    //afdrukken
    //begin punt van lantaarn, elke keer met de loop 10 opzij
    var xlantaarn = -30;
    //licht beginpunten
    var xlicht = -45;
    var zlicht = 62;

    //links van loaddock
    for (var i = 0; i < 3; i++) {
        //var lantaarn0

        naam[i] = StraatLantaarns(xlantaarn, 0, 42);
        lichtvar[i] = StraatLighting(xlicht, 8, zlicht);

        SpawnGroup.add(naam[i]);
        SpawnGroup.add(lichtvar[i]);
        xlantaarn += 10;
        xlicht += 13;
        zlicht += 2;
    };

    //rechts van loaddock

    //begin punt van lantaarn, elke keer met de loop 10 opzij
    var xlantaarn = 40;
    //licht beginpunten
    var xlicht = 60;
    var zlicht = 60;
    for (var i = 3; i < 6; i++) {
        //var lantaarn0

        naam[i] = StraatLantaarns(xlantaarn, 0, 42);
        lichtvar[i] = StraatLighting(xlicht, 8, zlicht);

        SpawnGroup.add(naam[i]);
        SpawnGroup.add(lichtvar[i]);
        xlantaarn += 10;
        xlicht += 13;
        zlicht -= 2;
    };

    //var lantaarn1 = StraatLantaarns(-30, 0, 42);
    //var lantaarn2 = StraatLantaarns(-20, 0, 42);

    //SpawnGroup.add(lantaarn1);
    //SpawnGroup.add(lantaarn2);

    //var licht1 = StraatLighting(-45, 8, 62);
    //var licht2 = StraatLighting(-32, 8, 64);

    ////licht toevoegen aan groep
    //SpawnGroup.add(licht1);
    //SpawnGroup.add(licht2);

    return SpawnGroup;


}