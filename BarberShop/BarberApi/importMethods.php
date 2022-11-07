<?php

function importEmploye($connect){
    if(move_uploaded_file($_FILES['Employers']['tmp_name'], '../files/'.$_FILES['Employers']['name'])){
     $responce=[
         "status"=>false,
         "message"=>"file not found"
     ];
    }

      if (!file_exists('../files/'.$_FILES['Employers']['name'])) {
          $responce=[
              "status"=>false,
              "message"=>"file not found"
          ];
          echo json_encode($responce); die();
      }
     try{
         $file =fopen("../files/".$_FILES['Employers']['name'], 'r');     
         $counter =0;       
         while (!feof($file)){
             $employer = fgetcsv($file,1024 ,';');
                //print_r( $employer);
             //print_r($employer);die();
             //$i = count($employer);
             if ($employer[1]!=null){
                 $createEmployee=$connect->prepare(
                     "insert into Employe( FirstName, LastName, MiddleName, Email, Password, INN, Post_ID, Status_ID) VALUES (?,?,?,?,?,?,?,?)");
                    // print_r($employer);
                     //if(empty($data["middlename"])) $data["middlename"]="-";
                     $createEmployee->execute(array(
                         strval($employer[0]), //firstName
                         strval($employer[1]), //lastname
                         strval($employer[2]), //middlename
                         strval($employer[3]),  //email
                         strval($employer[4]), //password
                         strval($employer[5]), //inn
                         $employer[6], //post_id
                         $employer[7] //status_id
                     ));


                     //print_r($employer); die();
                     $counter++;

             }
         }
         $responce=[
             "status"=>true,
             "message"=>"$counter user imported"
         ];
         echo json_encode($responce);
     } catch(PDOException  $e){
         $responce=[
             "status"=>false,
             "message"=>"employers are not imported"
         ];
         echo json_encode($responce);
     }
     fclose($file);
   //  unlink('../files/'.$_FILES['Employers']['name']);
 }
//////////////

