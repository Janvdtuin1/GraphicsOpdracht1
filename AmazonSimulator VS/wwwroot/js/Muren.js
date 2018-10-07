function Muren() {
    //muren rondom plane
    var muur = new THREE.BoxGeometry(30, 8.0, 0.5);
    var muurtextures = [
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/Muur/brick.png"), side: THREE.DoubleSide }), //LEFT
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/Muur/brick.png"), side: THREE.DoubleSide }), //RIGHT
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/Muur/beton.png"), side: THREE.DoubleSide }), //TOP
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/Muur/beton.png"), side: THREE.DoubleSide }), //BOTTOM
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/Muur/brick.png"), side: THREE.DoubleSide }), //FRONT
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/Muur/brick.png"), side: THREE.DoubleSide }), //BACK
    ];
    var muurmaterial = new THREE.MeshFaceMaterial(muurtextures);

        var rightwall = new THREE.Mesh(muur, muurmaterial);
        rightwall.position.set(15, 4, 0);
 
        var frontwall = new THREE.Mesh(muur, muurmaterial);
        frontwall.rotation.y = 92.69;
        frontwall.position.set(0, 4, 15);
    
        var backwall = new THREE.Mesh(muur, muurmaterial);
        backwall.rotation.y = 92.69;
    backwall.position.set(30, 4, 15);

    var MuurGroup = new THREE.Group()
    MuurGroup.add(rightwall, frontwall, backwall);

    return MuurGroup;

}; //einde muren

