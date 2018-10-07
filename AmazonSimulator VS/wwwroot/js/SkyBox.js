function SkyBox() {
    var skyboxgeometry = new THREE.BoxGeometry(1000, 1000, 1000);
    var skyboxMaterials = [
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/skybox/left.png"), side: THREE.DoubleSide }), //LEFT
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/skybox/right.png"), side: THREE.DoubleSide }), //RIGHT
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/skybox/up.png"), side: THREE.DoubleSide }), //TOP
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/skybox/down.png"), side: THREE.DoubleSide }), //BOTTOM
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/skybox/front.png"), side: THREE.DoubleSide }), //FRONT
        new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/skybox/back.png"), side: THREE.DoubleSide }), //BACK

    ];



    var skyboxMaterial = new THREE.MeshFaceMaterial(skyboxMaterials);
    var skybox = new THREE.Mesh(skyboxgeometry, skyboxMaterial);


    return skybox;
    
}