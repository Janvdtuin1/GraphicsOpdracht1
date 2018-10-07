function LoadDock() {
    var geometry = new THREE.BoxGeometry(30, 10, 10);
    var bricktexture = new THREE.TextureLoader().load("textures/Muur/brick.png");
    bricktexture.wrapS = THREE.RepeatWrapping;
    bricktexture.wrapT = THREE.RepeatWrapping;
    bricktexture.repeat.set(3, 1.5);

    var material = [
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/loaddock.jpg"), side: THREE.DoubleSide }), //LEFT
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/loaddock.jpg"), side: THREE.DoubleSide }), //RIGHT
        new THREE.MeshStandardMaterial({ map: bricktexture, side: THREE.DoubleSide }), //TOP
        new THREE.MeshStandardMaterial({ map: bricktexture, side: THREE.DoubleSide }), //TOP
        new THREE.MeshStandardMaterial({ map: bricktexture, side: THREE.DoubleSide }), //TOP
        new THREE.MeshStandardMaterial({ map: bricktexture, side: THREE.DoubleSide }), //TOP

    ];

    var cube = new THREE.Mesh(geometry, material);
    cube.position.set(15, 5, 35)
    return cube;
}

function RobotDoor()
{
    var doorgroup = new THREE.Group();
    var deur = new THREE.TextureLoader().load("textures/door.png");
    var geometry = new THREE.PlaneGeometry(4, 6, 10);
    var material = new THREE.MeshStandardMaterial({
        map: deur, side: THREE.DoubleSide
    });
    var deur1 = new THREE.Mesh(geometry, material);
   var deur2 = new THREE.Mesh(geometry, material);


    deur1.position.set(19, 3, 29.5);
    deur2.position.set(10, 3, 29.5);

    doorgroup.add(deur1);
    doorgroup.add(deur2);


    return doorgroup;
}