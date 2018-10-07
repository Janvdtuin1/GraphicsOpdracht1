function Plane() {
    var geometry = new THREE.PlaneGeometry(30, 30, 32);
    var material = new THREE.MeshStandardMaterial({ map: new THREE.TextureLoader().load("textures/beton.jpg"), side: THREE.DoubleSide });

    var plane = new THREE.Mesh(geometry, material);
    plane.rotation.x = Math.PI / 2.0;
    plane.position.x = 15;
    plane.position.z = 15;

    return plane;





}