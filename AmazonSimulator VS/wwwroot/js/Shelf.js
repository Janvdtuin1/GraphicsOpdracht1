function Stelling() {
    var stellingbox = new THREE.BoxGeometry(0.9, 4, 0.9);
    var stellingtexture = [
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/stelling.png"), side: THREE.DoubleSide }), //LEFT
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/stelling.png"), side: THREE.DoubleSide }), //RIGHT
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/oak_planks.png"), side: THREE.DoubleSide }), //TOP
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/oak_planks.png"), side: THREE.DoubleSide }), //BOTTOM
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/stelling.png"), side: THREE.DoubleSide }), //FRONT
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/stelling.png"), side: THREE.DoubleSide }), //BACK
    ];

    var stellingmateriaal = new THREE.MeshFaceMaterial(stellingtexture);
    var shelf = new THREE.Mesh(stellingbox, stellingmateriaal);

    return shelf;



}