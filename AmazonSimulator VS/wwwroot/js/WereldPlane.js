function Plane() {
    var geometry = new THREE.PlaneGeometry(30, 30, 32);
    var texture = new THREE.TextureLoader().load("textures/planks.jpg");
    var material = new THREE.MeshStandardMaterial({ map: texture, side: THREE.DoubleSide });
    texture.wrapS = THREE.RepeatWrapping;
    texture.wrapT = THREE.RepeatWrapping;
    texture.repeat.set(6, 6);

    var plane = new THREE.Mesh(geometry, material);
    plane.rotation.x = Math.PI / 2.0;
    plane.position.x = 15;
    plane.position.z = 15;

    return plane;





}