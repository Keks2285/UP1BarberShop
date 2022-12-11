<?php
//Проверь потом работает ли
function updateEmploye($connect, $data){
    try{
        $selectPosts=$connect->prepare("call Employe_Update(?,?,?,?,?,?,?,?)");
        $selectPosts->execute(array(
            strval($data["firstname"]),
            strval($data["lasttname"]),
            strval($data["middlename"]),
            strval($data["email"]),
            strval($data["inn"]),
            $data["post_id"],
            $data["status_id"],
            $data["id_employer"]
            )
        );
        $responce=[
         "status"=>true,
         "message"=>"employer updated"
        ];
        echo json_encode($responce);
        //die(); 
        // if(count($selectPosts->fetchAll())>0){
        //     $responce=[
        //         "status"=>false,
        //         "message"=>"post already exist"
        //     ];
        //     echo json_encode($responce);
        //     die();
        // }



        // $createPost=$connect->prepare(
        // "insert into Post (Name_Post, Price) VALUES 
        // (?,?)"
        // );
        
        // //if(empty($data["middlename"])) $data["middlename"]="-";
        // $createPost->execute(array(
        //     strval($data["name_post"]), 
        //     strval($data["price"])
        // ));
        // $responce=[
        //     "status"=>true,
        //     "message"=>"post created"
        // ];
        // echo json_encode($responce);
       // var_dump($data); die();
       // exit();
        //$addedEmploye=$createEmploye->fetchAll();
    } catch (PDOException $e) {
         $responce=[
            "status"=>false,
            "message"=>"employe not updated"
        ];
        echo json_encode($responce);
    }
}


function updatePost($connect, $data){
    try{
       // var_dump($data);
        $selectPosts=$connect->prepare("call Post_Update(?,?,?)");
        $selectPosts->execute(array(
            $data["id_post"],
            strval($data["namepost"]),
            $data["price"],
            )
        );
        $responce=[
         "status"=>true,
         "message"=>"post updated"
        ];
        echo json_encode($responce);
        //die(); 
        // if(count($selectPosts->fetchAll())>0){
        //     $responce=[
        //         "status"=>false,
        //         "message"=>"post already exist"
        //     ];
        //     echo json_encode($responce);
        //     die();
        // }



        // $createPost=$connect->prepare(
        // "insert into Post (Name_Post, Price) VALUES 
        // (?,?)"
        // );
        
        // //if(empty($data["middlename"])) $data["middlename"]="-";
        // $createPost->execute(array(
        //     strval($data["name_post"]), 
        //     strval($data["price"])
        // ));
        // $responce=[
        //     "status"=>true,
        //     "message"=>"post created"
        // ];
        // echo json_encode($responce);
       // var_dump($data); die();
       // exit();
        //$addedEmploye=$createEmploye->fetchAll();
    } catch (PDOException $e) {
         $responce=[
            "status"=>false,
            "message"=>"post not updated"
        ];
        echo json_encode($responce);
    }
}