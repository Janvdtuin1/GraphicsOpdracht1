function LoadDock() {
    var geometry = new THREE.BoxGeometry(30, 10, 10);
    var material = [
        new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/deur.jpg"), side: THREE.DoubleSide }), //LEFT
        new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/deur.jpg"), side: THREE.DoubleSide }), //RIGHT
        new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/grijs.jpg"), side: THREE.DoubleSide }), //TOP
        new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/grijs.jpg"), side: THREE.DoubleSide }), //TOP
        new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/grijs.jpg"), side: THREE.DoubleSide }), //TOP
        new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/grijs.jpg"), side: THREE.DoubleSide }), //TOP




    ];
    var cube = new THREE.Mesh(geometry, material);
    cube.position.set(15, 5, 35)
    return cube;
}